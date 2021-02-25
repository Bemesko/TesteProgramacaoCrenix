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
        IsEmpty = false;
        gear.CurrentGearPlacement = this;
        gear.CurrentUISlot = null;
        gear.WasDragSuccessful = true;
        gear.GetComponent<RectTransform>().SetParent(gameObject.GetComponent<RectTransform>());
        gear.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        gear.ChangeGearAppearence(Gear.Appearance.PlayZone);
        Debug.Log(GearPlacementManager.Instance.CheckIfShouldSpin());
    }
}
