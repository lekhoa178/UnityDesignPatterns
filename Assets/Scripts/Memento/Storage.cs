using System;
using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour
{
    [Tooltip("Static storage cannot be swapped out - for Ability Trees, Spellbooks, etc")]
    [SerializeField] public bool staticStorage;

    [SerializeField] protected List<UISlot> slots = new List<UISlot>();
    [SerializeField] protected List<Item> items = new List<Item>();

    UISlot swapUISlot;

    private void Start()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            slots[i].UpdateUI(items[i]);
            slots[i].SetupStorage(this);
            slots[i].SetupMouseDrag(this);
        }
    }
    public void SwapItem(UISlot slot)
    {
        if (swapUISlot == null)
        {
            swapUISlot = slot;
        }
        else if (swapUISlot == slot)
        {
            swapUISlot = null;
        }
        else
        {
            Storage storage1 = swapUISlot.GetStorage();
            int index1 = storage1.GetItemIndex(swapUISlot);
            Item item1 = storage1.GetItem(index1);

            Storage storage2 = slot.GetStorage();
            int index2 = storage1.GetItemIndex(slot);
            Item item2 = storage1.GetItem(index2);

            if (!storage1.staticStorage)
            {
                storage1.SetItemSlot(index1, item2);
                swapUISlot.UpdateUI(item2);
            }

            if (!storage2.staticStorage)
            {
                storage2.SetItemSlot(index2, item1);
                slot.UpdateUI(item1);
            }

            swapUISlot = null;
        }
    }

    public void ClearSwap() => swapUISlot = null;

    int GetItemIndex(UISlot slot) => slots.IndexOf(slot);
    Item GetItem(int index) => items[index];
    void SetItemSlot(int index, Item item) => items[index] = item;
}