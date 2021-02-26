using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearPlacementManager : MonoBehaviour
{
    public static GearPlacementManager Instance;
    public delegate void SlotsFilledAction(bool isSlotsFilled);
    public static event SlotsFilledAction OnSlotsFilled;

    [SerializeField]
    private GearPlacement[] _gearPlacements;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        for (int i = 0; i < _gearPlacements.Length; i++)
        {
            _gearPlacements[i].SlotID = i;
            //Slots de cima giram no sentido horário
            _gearPlacements[i].TurnsClockwise = i % 2 == 0;
        }
    }

    public void CheckIfAllSlotsFull()
    {
        bool allSlotsFull = true;

        foreach (GearPlacement gearPlacement in _gearPlacements)
        {
            if (gearPlacement.IsEmpty)
                allSlotsFull = false;
        }

        OnSlotsFilled(allSlotsFull);
    }

    public void SpinAllGears()
    {
        foreach (GearPlacement _gearPlacement in _gearPlacements)
        {
            _gearPlacement.GetGear().SetGearSpin(true);
        }
    }
}
