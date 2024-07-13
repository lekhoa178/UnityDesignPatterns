using System;
using System.Reflection;
using UnityEngine;

[CreateAssetMenu(fileName = "PowerUp", menuName = "PowerUp")]
public class PowerUp : ScriptableObject, IVisitor
{
    public int HealthBonus = 10;
    public int ManaBonus = 10;

    // Performance Killer
    public void Visit(object o)
    {
        MethodInfo visitMethod = GetType().GetMethod("Visit", new Type[] { o.GetType() });
        if (visitMethod != null && visitMethod != GetType().GetMethod("Visit", new Type[] { o.GetType() }))
        {
            visitMethod.Invoke(this, new object[] { o });
        }
        else
        {
            DefaultVisit(o);
        }
    }

    private void DefaultVisit(object o)
    {
        Debug.Log("Powerup.DefaultVisit");
    }

    public void Visit(HealthComponent healthComponent)
    {
        healthComponent.AddHealth(HealthBonus);
        Debug.Log("PowerUp.Visit(HealthComponent)");
    }

    public void Visit(ManaComponent manaComponent)
    {
        manaComponent.AddMana(ManaBonus);
        Debug.Log("PowerUp.Visit(ManaComponent)");
    }
}
