using Sirenix.OdinInspector;
using UnityEngine;

public abstract class Entity : MonoBehaviour, Mediator.IVisitable
{
    [SerializeField, InlineEditor, Required] BaseStats baseStats;

    Stats stats;

    public StatsMediator StatsMediator => stats.Mediator;

    private void Awake()
    {
        stats = new Stats(new StatsMediator(), baseStats);
    }

    private void Update()
    {
        stats.Mediator.Update(Time.deltaTime);
    }

    public void Accept(Mediator.IVisitor visitor) => visitor.Visit(this);
}