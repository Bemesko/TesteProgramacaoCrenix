using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    private RectTransform _spriteTransform;

    private void Awake()
    {
        _spriteTransform = GetComponentInChildren<RectTransform>();
    }
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Dropped");
        if (eventData.pointerDrag != null)
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = _spriteTransform.anchoredPosition;
    }
}
