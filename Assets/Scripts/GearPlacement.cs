using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GearPlacement : MonoBehaviour, IDropHandler
{
    public int SlotID;
    public bool TurnsClockwise;
    public bool IsEmpty = true;

    private RectTransform _spriteTransform;
    private Gear _currentGear;

    private void Awake()
    {
        _spriteTransform = GetComponentInChildren<RectTransform>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        Gear draggedGear = eventData.pointerDrag.GetComponent<Gear>();

        Debug.Log($"Dropped in slot {SlotID}");

        if (draggedGear != null && IsEmpty)
        {
            FillSlot(draggedGear);
        }
        else
        {
            Debug.Log("Esse slot já está cheio");
        }
    }

    public void FillSlot(Gear gear)
    {
        _currentGear = gear;
        IsEmpty = false;
        _currentGear.CurrentGearPlacement = this;
        _currentGear.CurrentUISlot = null;
        _currentGear.WasDragSuccessful = true;
        _currentGear.GetComponent<RectTransform>().SetParent(gameObject.GetComponent<RectTransform>());
        _currentGear.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        _currentGear.ChangeGearAppearence(Gear.Appearance.PlayZone);
        _currentGear.SetGearSpinDirection(TurnsClockwise);
        GearPlacementManager.Instance.CheckIfAllSlotsFull();
    }

    public Gear GetGear()
    {
        return _currentGear;
    }
}
