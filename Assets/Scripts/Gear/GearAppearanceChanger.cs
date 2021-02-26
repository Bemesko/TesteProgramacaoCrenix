using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GearAppearanceChanger : MonoBehaviour
{
    [SerializeField] private Sprite _UIGearSprite;
    [SerializeField] private Sprite _PZGearSprite;

    private Image _gearImage;
    private RectTransform _gearRectTransform;

    private void Awake()
    {
        _gearImage = GetComponentInChildren<Image>();
        _gearRectTransform = GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        GearSlot.OnGearSlotFilled += ChangeAppearanceToPlayzone;
        InventorySlot.OnUISlotFilled += ChangeAppearanceToUI;
    }

    private void OnDisable()
    {
        GearSlot.OnGearSlotFilled -= ChangeAppearanceToPlayzone;
        InventorySlot.OnUISlotFilled -= ChangeAppearanceToUI;
    }

    private void ChangeAppearanceToPlayzone(GearSlot gearSlot, Gear gear)
    {
        if (gear.GearAppearance == this)
        {
            _gearImage.sprite = _PZGearSprite;
            _gearRectTransform.localScale = new Vector3(1.5f, 1.5f, 1);
        }
    }

    private void ChangeAppearanceToUI(InventorySlot inventory, Gear gear)
    {
        if (gear.GearAppearance == this)
        {
            _gearImage.sprite = _UIGearSprite;
            _gearRectTransform.localScale = new Vector3(1f, 1f, 1);
        }
    }
}
