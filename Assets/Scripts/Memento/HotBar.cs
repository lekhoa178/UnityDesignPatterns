using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine.Rendering.Universal;

public class HotBar : Storage
{
    public Memento CreateMemento() => new Memento(items);

    public void SetMemento(Memento memento)
    {
        items = memento.GetItems();
        for (int i = 0; i < items.Count; i++)
        {
            slots[i].UpdateUI(items[i]);
        }
    }

    public class Memento
    {
        List<Item> items;

        public Memento(List<Item> items)
        {
            this.items = items;
        }

        public List<Item> GetItems() => items;
    }
}
