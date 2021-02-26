using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GearDragNDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private Canvas _canvas;

    private bool _wasDragSuccessful = false;
    private Slot _currentSlot;
    private RectTransform _gearRectTransform;
    private Vector2 _initialDragPosition;
    private Transform _beginDragParent;
    private Slot _beginDragSlot;
    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _gearRectTransform = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    private void OnEnable()
    {
        GearSlot.OnGearSlotFilled += DropInGearSlot;
        InventorySlot.OnUISlotFilled += DropInInventorySlot;
    }

    private void OnDisable()
    {

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _beginDragParent = _gearRectTransform.parent;
        _beginDragSlot = _currentSlot;

        if (_currentSlot != null)
            _currentSlot.IsEmpty = true;

        GearSlotManager.Instance.CheckIfAllSlotsFull();

        _wasDragSuccessful = false;
        _initialDragPosition = _gearRectTransform.anchoredPosition;

        _gearRectTransform.SetParent(_canvas.GetComponent<RectTransform>());

        _canvasGroup.alpha = .6f;
        _canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _gearRectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _canvasGroup.alpha = 1f;
        _canvasGroup.blocksRaycasts = true;

        if (!_wasDragSuccessful)
        {

            if (_currentSlot != null)
                _currentSlot.IsEmpty = false;

            _gearRectTransform.SetParent(_beginDragParent);
            _gearRectTransform.anchoredPosition = new Vector2(0, 0);
        }

        GearSlotManager.Instance.CheckIfAllSlotsFull();
    }

    private void DropInGearSlot(GearSlot gearSlot, Gear gear)
    {
        if (gear.GearDragDrop == this)
        {
            _currentSlot = gearSlot;
            _wasDragSuccessful = true;
            _gearRectTransform.SetParent(gearSlot.GetComponent<RectTransform>());
            _gearRectTransform.anchoredPosition = new Vector2(0, 0);
        }
    }

    private void DropInInventorySlot(InventorySlot inventorySlot, Gear gear)
    {
        if (gear.GearDragDrop == this)
        {
            _currentSlot = inventorySlot;
            _wasDragSuccessful = true;
            _gearRectTransform.SetParent(inventorySlot.GetComponent<RectTransform>());
            _gearRectTransform.anchoredPosition = new Vector2(0, 0);
        }
    }
}
