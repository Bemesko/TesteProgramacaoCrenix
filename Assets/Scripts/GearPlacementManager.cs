using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearPlacementManager : MonoBehaviour
{
    [SerializeField]
    private GearPlacement[] _gearPlacements;

    public static GearPlacementManager Instance;

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
            _gearPlacements[i].TurnsClockwise = i % 2 == 0;
        }
    }

    public bool CheckIfShouldSpin()
    {
        bool shouldSpin = true;

        foreach (GearPlacement gearPlacement in _gearPlacements)
        {
            if (gearPlacement.IsEmpty)
                shouldSpin = false;
        }

        return shouldSpin;
    }
}
