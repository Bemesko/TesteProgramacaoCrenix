using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearSlotManager : MonoBehaviour
{
    public static GearSlotManager Instance;
    public delegate void AllSlotsFilledAction(bool isAllSlotsFilled);
    public static event AllSlotsFilledAction OnAllSlotsFilled;

    [SerializeField]
    private GearSlot[] _gearSlots;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        for (int i = 0; i < _gearSlots.Length; i++)
        {
            _gearSlots[i].SlotID = i;
            //Slots de cima giram no sentido horário
            _gearSlots[i].TurnsClockwise = i % 2 == 0;
        }
    }

    public void CheckIfAllSlotsFull()
    {
        bool allSlotsFull = true;

        foreach (GearSlot gearPlacement in _gearSlots)
        {
            if (gearPlacement.IsEmpty)
                allSlotsFull = false;
        }

        OnAllSlotsFilled(allSlotsFull);
    }
}
