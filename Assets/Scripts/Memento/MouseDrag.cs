using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityUtils;

public class MouseDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Storage storage;
    UISlot slot;
    GameObject dragInstance;

    public void SetupStorage(Storage storage, UISlot slot)
    {
        this.storage = storage;
        this.slot = slot;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        storage.SwapItem(slot);

        dragInstance = new GameObject("Drag: " + slot.name);
        Image image = dragInstance.GetOrAdd<Image>();

        image.sprite = slot.itemImage.sprite;
        image.raycastTarget = false;

        dragInstance.transform.SetParent(slot.transform);
        dragInstance.transform.localScale = Vector3.one;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (dragInstance != null)
        {
            dragInstance.transform.position = Input.mousePosition;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject is GameObject targetObj)
        {
            UISlot targetSlot = targetObj.GetComponentInParent<UISlot>();
            if (targetSlot != null)
            {
                storage.SwapItem(targetSlot);
                EventSystem.current.SetSelectedGameObject(targetObj);
            }
            
        }

        storage.ClearSwap();

        if (dragInstance != null)
            Destroy(dragInstance);
    }
}