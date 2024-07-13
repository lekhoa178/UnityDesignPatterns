using System;
using UnityEngine;
using UnityServiceLocator;

public class StatModifierPickup : Pickup
{
    public enum OperatorType { Add, Multiply }

    [SerializeField] StatType type = StatType.Attack;
    [SerializeField] OperatorType operatorType = OperatorType.Add;
    [SerializeField] int value = 10;
    [SerializeField] float duration = 5f;

    //protected override void ApplyPickupEffect(IEntity entity)
    //{
    //    StatModifier modifier = operatorType switch
    //    {
    //        OperatorType.Add => new BasicStatModifer(type, duration, v => v + value),
    //        OperatorType.Multiply => new BasicStatModifer(type, duration, v => v * value),
    //        _ => throw new ArgumentOutOfRangeException()
    //    };

    //    entity.Stats.Mediator.AddModifier(modifier);
    //}

    protected override void ApplyPickupEffect(Entity entity)
    {
        // Not registered yet, design a way
        StatModifier modifier = ServiceLocator.For(this).Get<IStatModifierFactory>().Create(operatorType, type, value, duration);
        entity.StatsMediator.AddModifier(modifier);
    }
}

public abstract class Pickup : MonoBehaviour, Mediator.IVisitor
{
    protected abstract void ApplyPickupEffect(Entity entity);


    public void Visit<T>(T visitable) where T : Component, Mediator.IVisitable
    {
        if (visitable is Entity entity)
        {
            ApplyPickupEffect(entity);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        other.GetComponent<Mediator.IVisitable>().Accept(this);
        Destroy(gameObject);
    }
}