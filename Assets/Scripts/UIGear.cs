using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIGear : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public InventorySlot CurrentUISlot;
    public GearPlacement CurrentGearPlacement;
    public bool WasDragSuccessful = false;

    [SerializeField] private Canvas _canvas;

    private Vector2 _initialDragPosition;
    private RectTransform _rectTransform;
    private Transform _beginDragParent;
    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();
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
}
