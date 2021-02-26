using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Gear : MonoBehaviour
{
    public GearDragNDrop GearDragDrop;
    public GearAppearanceChanger GearAppearance;
    public GearSpinner GearSpinner;

    private void Awake()
    {
        GearDragDrop = GetComponent<GearDragNDrop>();
        GearAppearance = GetComponent<GearAppearanceChanger>();
        GearSpinner = GetComponent<GearSpinner>();
    }
}
