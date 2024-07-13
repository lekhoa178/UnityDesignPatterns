using System;
using System.Collections.Generic;
using UnityUtils;

namespace PathFinding
{
    public class Node<T>
    {

        public T Data { get; set; }

        // Calculate the "real" cost from one node to another node
        public Func<Node<T>, Node<T>, float> Cost { get; set; }

        // Estimate the cost from one node to another node
        public Func<Node<T>, Node<T>, float> Heuristic { get; set; }

        public float G { get; set; } // Real cost from start to this node
        public float RHS { get; set; } // Estimated cost from start to this node
        public bool GEqualRHS => G.Approx(RHS);

        public List<Node<T>> Neighbors { get; set; } = new();

        public Node(T data, Func<Node<T>, Node<T>, float> cost, Func<Node<T>, Node<T>, float> heuristic)
        {
            Data = data;
            Cost = cost;
            Heuristic = heuristic;

            G = float.MaxValue;
            RHS = float.MaxValue;
        }
    }

    public class DStarLite<T>
    {
        readonly Node<T> startNode;
        readonly Node<T> goalNode;
        readonly List<Node<T>> allNodes;
        float km; // Key modifier

        // SortedSet will
        readonly SortedSet<((float, float), Node<T>)> openSet = new();
        readonly Dictionary<Node<T>, (float, float)> lookups = new();
    }
}