
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public interface IGoapPlanner
{
    ActionPlan Plan(GoapAgent agent, HashSet<AgentGoal> goals, AgentGoal mostRecentGoal = null);
}

public class GoapPlanner : IGoapPlanner
{
    private float startTime = 0;

    public ActionPlan Plan(GoapAgent agent, HashSet<AgentGoal> goals, AgentGoal mostRecentGoal = null)
    {
        startTime = (float)Time.realtimeSinceStartupAsDouble;
        // Order goal by priority, descending
        List<AgentGoal> orderedGoals = goals
            .Where(g => g.DesiredEffects.Any(b => !b.Evaluate()))
            .OrderByDescending(g => g == mostRecentGoal ? g.Priority - 0.01 : g.Priority)
            .ToList();

        // Try to solve each goal in order
        foreach (var goal in orderedGoals)
        {
            Node goalNode = new Node(null, null, goal.DesiredEffects, 0);

            // If we can find a path to the goal, return the plan
            //if (FindPath(goalNode, agent.actions))
            //{
            //    // If the goalNode has no leaves and no action to perform try a different goal
            //    if (goalNode.IsLeafDead) continue;

            //    Stack<AgentAction> actionStack = new Stack<AgentAction>();
            //    while (goalNode.Leaves.Count > 0)
            //    {
            //        var cheapestLeaf = goalNode.Leaves.OrderBy(leaf => leaf.Cost).First();
            //        goalNode = cheapestLeaf;
            //        actionStack.Push(cheapestLeaf.Action);
            //    }

            //    return new ActionPlan(goal, actionStack, goalNode.Cost);
            //}

            // A* version
            if (FindPath(goalNode, agent.actions, out Node startNode))
            {
                // If the goalNode has no leaves and no action to perform try a different goal
                if (goalNode == startNode) continue;

                Stack<AgentAction> actionStack = new Stack<AgentAction>();
                Stack<AgentAction> tempStack = new Stack<AgentAction>();

                // Traverse from the goal node to the start node and push each action onto the temp stack
                Node currentNode = startNode;
                while (currentNode != goalNode)
                {
                    tempStack.Push(currentNode.Action);
                    currentNode = currentNode.Parent;
                }

                // Pop actions from the temp stack and push them onto the action stack
                while (tempStack.Count > 0)
                {
                    actionStack.Push(tempStack.Pop());
                }

                Debug.Log($"Delta Time: {(Time.realtimeSinceStartupAsDouble - startTime) * 1000}");
                return new ActionPlan(goal, actionStack, goalNode.Cost);
            }
        }

        Debug.Log($"Delta Time: {(Time.time - startTime) * 1000}");
        Debug.LogWarning("No plan found");
        return null;
    }

    // Depth first search version
    bool FindPath(Node parent, HashSet<AgentAction> actions)
    {
        // Order actions by cost, ascending
        var orderedActions = actions.OrderBy(a => a.Cost);

        foreach (var action in orderedActions)
        {
            var requiredEffects = parent.RequiredEffects;

            // Remove any effects that evaluate to true, there is no action to take
            requiredEffects.RemoveWhere(b => b.Evaluate());

            // If there are no required effects to fulfill, we have a plan
            if (requiredEffects.Count == 0)
            {
                return true;
            }

            if (action.Effects.Any(requiredEffects.Contains))
            {
                var newRequiredEffects = new HashSet<AgentBelief>(requiredEffects);
                newRequiredEffects.ExceptWith(action.Effects);
                newRequiredEffects.UnionWith(action.Preconditions);

                var newAvalaibleActions = new HashSet<AgentAction>(actions);
                newAvalaibleActions.Remove(action);

                var newNode = new Node(parent, action, newRequiredEffects, parent.Cost + action.Cost);

                // Explore the new node recursively
                if (FindPath(newNode, newAvalaibleActions))
                {
                    parent.Leaves.Add(newNode);
                    newRequiredEffects.ExceptWith(newNode.Action.Preconditions);
                }

                // If all effects at this depth have been satisfied, return true
                if (newRequiredEffects.Count == 0)
                {
                    return true;
                }
            }
        }

        return false;
    }

    // A* version
    bool FindPath(Node goalNode, HashSet<AgentAction> actions, out Node startNode)
    {
        startNode = null;
        var openList = new List<Node>();
        var closedList = new HashSet<Node>();
        openList.Add(goalNode);

        while (openList.Count > 0)
        {
            var currentNode = openList.OrderBy(node => node.Cost + Heuristic(node)).First();

            // If all required effects are satisfied, we have a plan
            if (currentNode.RequiredEffects.All(effect => effect.Evaluate()))
            {
                startNode = currentNode;
                return true;
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            foreach (var action in actions)
            {
                if (!action.Effects.Any(currentNode.RequiredEffects.Contains)) continue;

                var newRequiredEffects = new HashSet<AgentBelief>(currentNode.RequiredEffects);
                newRequiredEffects.ExceptWith(action.Effects);
                newRequiredEffects.UnionWith(action.Preconditions);

                var newNode = new Node(currentNode, action, newRequiredEffects, currentNode.Cost + action.Cost);

                if (closedList.Contains(newNode)) continue;

                if (!openList.Contains(newNode))
                {
                    openList.Add(newNode);
                    currentNode.Leaves.Add(newNode);
                }
            }
        }

        return false;
    }

    float Heuristic(Node node)
    {
        return node.RequiredEffects.Count;
    }

    float Heuristic(Node node, HashSet<AgentAction> actions)
    {
        // Number of unsatisfied effects
        int remainingEffectsCount = node.RequiredEffects.Count;

        // Estimate of the cheapest cost to fulfill each remaining effect
        float estimatedCost = 0f;
        foreach (var effect in node.RequiredEffects)
        {
            var cheapestAction = actions
                .Where(action => action.Effects.Contains(effect))
                .OrderBy(action => action.Cost)
                .FirstOrDefault();

            if (cheapestAction != null)
            {
                estimatedCost += cheapestAction.Cost;
            }
            else
            {
                // If no action can fulfill this effect, the heuristic is very high
                estimatedCost += float.MaxValue;
            }
        }

        // Combining the remaining effects count and estimated cost
        return remainingEffectsCount + estimatedCost;
    }
}

public class Node
{
    public Node Parent { get; }
    public AgentAction Action { get; }
    public HashSet<AgentBelief> RequiredEffects { get; }
    public List<Node> Leaves { get; }
    public float Cost { get; }

    public bool IsLeafDead => Leaves.Count == 0 && Action == null;

    public Node(Node parent, AgentAction action, HashSet<AgentBelief> effects, float cost)
    {
        Parent = parent;
        Action = action;
        RequiredEffects = new HashSet<AgentBelief>(effects);
        Leaves = new List<Node>();
        Cost = cost;
    }
}

public class ActionPlan
{
    public AgentGoal AgentGoal { get; }
    public Stack<AgentAction> Actions { get; }
    public float TotalCost { get; }

    public ActionPlan(AgentGoal agentGoal, Stack<AgentAction> actions, float totalCost)
    {
        AgentGoal = agentGoal;
        Actions = actions;
        TotalCost = totalCost;
    }

}