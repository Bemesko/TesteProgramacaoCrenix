using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearSpinner : MonoBehaviour
{
    private Animator _gearAnimator;

    private void Awake()
    {
        _gearAnimator = GetComponentInChildren<Animator>();
    }

    private void OnEnable()
    {
        GearSlotManager.OnAllSlotsFilled += SetGearSpin;
        GearSlot.OnGearSlotFilled += SetGearSpinDirection;
    }

    private void OnDisable()
    {
        GearSlotManager.OnAllSlotsFilled -= SetGearSpin;
        GearSlot.OnGearSlotFilled -= SetGearSpinDirection;
    }

    public void SetGearSpin(bool isSpin)
    {
        _gearAnimator.SetBool("IsSpinning", isSpin);
    }

    public void SetGearSpinDirection(GearSlot gearSlot, Gear gear)
    {
        if (gear.GearSpinner == this)
            _gearAnimator.SetBool("IsClockwise", gearSlot.TurnsClockwise);
    }
}
