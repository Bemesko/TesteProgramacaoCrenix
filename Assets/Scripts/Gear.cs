using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Gear : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public enum Appearance
    {
        UI,
        PlayZone
    }

    public InventorySlot CurrentUISlot;
    public GearPlacement CurrentGearPlacement;
    public bool WasDragSuccessful = false;

    [SerializeField] private Canvas _canvas;
    [SerializeField] private Sprite _UIGearSprite;
    [SerializeField] private Sprite _PZGearSprite;

    private Vector2 _initialDragPosition;
    private RectTransform _rectTransform;
    private Transform _beginDragParent;
    private CanvasGroup _canvasGroup;
    private Image _gearImage;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();
        _gearImage = GetComponentInChildren<Image>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (CurrentUISlot != null)
            CurrentUISlot.IsEmpty = true;

        if (CurrentGearPlacement != null)
            CurrentGearPlacement.IsEmpty = true;

        WasDragSuccessful = false;
        _initialDragPosition = _rectTransform.anchoredPosition;

        _beginDragParent = _rectTransform.parent;
        _rectTransform.SetParent(_canvas.GetComponent<RectTransform>());

        _canvasGroup.alpha = .6f;
        _canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _canvasGroup.alpha = 1f;
        _canvasGroup.blocksRaycasts = true;

        if (!WasDragSuccessful)
        {
            _rectTransform.SetParent(_beginDragParent);
            _rectTransform.anchoredPosition = new Vector2(0, 0);
        }
    }

    public void ChangeGearAppearence(Appearance appearanceID)
    {
        switch (appearanceID)
        {
            case Appearance.PlayZone:
                _gearImage.sprite = _PZGearSprite;
                _rectTransform.localScale = new Vector3(1.5f, 1.5f, 1);
                break;

            case Appearance.UI:
                _gearImage.sprite = _UIGearSprite;
                _rectTransform.localScale = new Vector3(1f, 1f, 1);
                break;
        }
    }
}
