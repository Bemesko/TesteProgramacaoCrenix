using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIGear : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public InventorySlot CurrentUISlot;
    public bool WasDragSuccessful = false;

    [SerializeField] private Canvas _canvas;

    private Vector2 _initialDragPosition;
    private RectTransform _rectTransform;
    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (CurrentUISlot != null)
        {
            CurrentUISlot.IsEmpty = true;
        }

        WasDragSuccessful = false;
        _initialDragPosition = _rectTransform.anchoredPosition;

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
            _rectTransform.anchoredPosition = _initialDragPosition;
    }
}
