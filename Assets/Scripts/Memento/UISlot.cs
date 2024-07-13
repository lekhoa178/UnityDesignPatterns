using NUnit.Framework;
using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityUtils;

public class UISlot : MonoBehaviour
{
    public Image itemImage;
    Storage storage;
    MouseDrag mouseDrag;

    public void SetupStorage(Storage storage)
    {
        this.storage = storage;
    }

    public Storage GetStorage()
    {
        return storage;
    }

    public void UpdateUI(Item item)
    {
        if (item == null)
        {
            item.sprite = null;
            return;
        }

        itemImage.sprite = item.sprite;
    }

    public void SetupMouseDrag(Storage storage)
    {
        mouseDrag = gameObject.GetOrAdd<MouseDrag>();
        mouseDrag.SetupStorage(storage, this);
    }
}
