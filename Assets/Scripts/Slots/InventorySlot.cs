using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : Slot, IDropHandler
{
    public delegate void FillUISlotAction(InventorySlot inventorySlot, Gear gear);
    public static event FillUISlotAction OnUISlotFilled;

    public override void FillSlot(Gear gear)
    {
        IsEmpty = false;
        OnUISlotFilled(this, gear);
    }
}
