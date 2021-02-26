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
    private GearPlacement _beginDragPlacement;
    private InventorySlot _beginDragInventorySlot;
    private CanvasGroup _canvasGroup;
    private Image _gearImage;
    private Animator _gearAnimator;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();
        _gearImage = GetComponentInChildren<Image>();
        _gearAnimator = GetComponentInChildren<Animator>();
    }

    private void OnEnable()
    {
        GearPlacementManager.OnSlotsFilled += SetGearSpin;
    }

    private void OnDisable()
    {
        GearPlacementManager.OnSlotsFilled -= SetGearSpin;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _beginDragParent = _rectTransform.parent;
        _beginDragInventorySlot = CurrentUISlot;
        _beginDragPlacement = CurrentGearPlacement;

        if (CurrentUISlot != null)
            CurrentUISlot.IsEmpty = true;

        if (CurrentGearPlacement != null)
            CurrentGearPlacement.IsEmpty = true;

        GearPlacementManager.Instance.CheckIfAllSlotsFull();

        WasDragSuccessful = false;
        _initialDragPosition = _rectTransform.anchoredPosition;

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
            if (CurrentUISlot != null)
                CurrentUISlot.IsEmpty = false;

            if (CurrentGearPlacement != null)
                CurrentGearPlacement.IsEmpty = false;

            _rectTransform.SetParent(_beginDragParent);
            _rectTransform.anchoredPosition = new Vector2(0, 0);
        }

        GearPlacementManager.Instance.CheckIfAllSlotsFull();
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

    public void SetGearSpin(bool isSpin)
    {
        _gearAnimator.SetBool("IsSpinning", isSpin);
    }

    public void SetGearSpinDirection(bool isClockwise)
    {
        _gearAnimator.SetBool("IsClockwise", isClockwise);
    }
}
