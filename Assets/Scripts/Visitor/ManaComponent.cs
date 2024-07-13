using Sirenix.OdinInspector;
using UnityEngine;

public class ManaComponent : MonoBehaviour, IVisitable
{
    [ShowInInspector] public int Mana { get; private set; }

    public void Accept(IVisitor visitor)
    {
        visitor.Visit(this);
        Debug.Log("Mana Health Component.Accept");
    }

    public void AddMana(int mana)
    {
        this.Mana = mana; 
    }
}