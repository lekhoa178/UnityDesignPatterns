using Sirenix.OdinInspector;
using UnityEngine;

public class HealthComponent : MonoBehaviour, IVisitable
{
    [ShowInInspector] public int Health { get; private set; }

    public void Accept(IVisitor visitor)
    {
        visitor.Visit(this);
        Debug.Log("Health Component.Accept");
    }

    public void AddHealth(int health)
    {
        this.Health = health;
    }
}
