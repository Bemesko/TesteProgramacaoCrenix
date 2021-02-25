﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public int _slotID;
    public bool IsEmpty = true;

    private RectTransform _spriteTransform;

    private void Awake()
    {
        _spriteTransform = GetComponentInChildren<RectTransform>();
    }
    public void OnDrop(PointerEventData eventData)
    {
        UIGear draggedGear = eventData.pointerDrag.GetComponent<UIGear>();

        Debug.Log($"Dropped in slot {_slotID}");

        if (draggedGear != null && IsEmpty)
        {
            FillSlot(draggedGear);
        }
        else
        {
            Debug.Log("Esse slot já está cheio");
        }
    }

    public void FillSlot(UIGear gear)
    {
        IsEmpty = false;
        gear.CurrentUISlot = this;
        gear.CurrentGearPlacement = null;
        gear.WasDragSuccessful = true;
        gear.GetComponent<RectTransform>().SetParent(gameObject.GetComponent<RectTransform>());
        gear.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
    }
}
