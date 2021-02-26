using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GearSlot : Slot, IDropHandler
{
    public delegate void FillGearSlotAction(GearSlot gearSlot, Gear gear);
    public static event FillGearSlotAction OnGearSlotFilled;

    public int SlotID;
    public bool TurnsClockwise;
    private Gear _currentGear;

    public override void FillSlot(Gear gear)
    {
        _currentGear = gear;
        IsEmpty = false;
        OnGearSlotFilled(this, gear);
        GearSlotManager.Instance.CheckIfAllSlotsFull();
    }

    public void GetGearSpinner()
    {
        Debug.Log("Speen");
    }
}
