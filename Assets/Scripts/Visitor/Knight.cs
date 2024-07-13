using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityUtils;

public class Knight : MonoBehaviour, IVisitable
{
    List<IVisitable> visitableComponents = new List<IVisitable>();

    private void Start()
    {
        visitableComponents.Add(gameObject.GetOrAdd<HealthComponent>());
        visitableComponents.Add(gameObject.GetOrAdd<ManaComponent>());
    }

    public void Accept(IVisitor visitor)
    {
        foreach (var visitable in visitableComponents)
        {
            visitable.Accept(visitor);
        }
    }
}
