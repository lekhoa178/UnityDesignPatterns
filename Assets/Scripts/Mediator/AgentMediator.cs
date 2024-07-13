
using UnityEngine;
using UnityServiceLocator;

public class AgentMediator : Mediator<GoapAgent>
{
    private void Awake() => ServiceLocator.Global.Register(this as Mediator<GoapAgent>);
    protected override bool MediatorConditionMet(GoapAgent target) => true;
    //protected override bool MediatorConditionMet(GoapAgent target) => target.Status == AgentStatus.Active;

    protected override void OnRegistered(GoapAgent entity)
    {
        Debug.Log($"{entity.name} registered");
        Broadcast(entity, new MessagePayload.Builder(entity).WithContent("Registered").Build());
    }

    protected override void OnDeregistered(GoapAgent entity)
    {
        Debug.Log($"{entity.name} deregistered");
        Broadcast(entity, new MessagePayload.Builder(entity).WithContent("Deregistered").Build());
    }

    // Add addtional logic here
}