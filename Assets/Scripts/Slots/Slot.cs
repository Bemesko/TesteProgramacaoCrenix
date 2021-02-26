using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler
{

    public bool IsEmpty = true;
    private RectTransform _spriteTransform;

    private void Awake()
    {
        _spriteTransform = GetComponentInChildren<RectTransform>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        Gear draggedGear = eventData.pointerDrag.GetComponent<Gear>();

        if (IsEmpty)
        {
            FillSlot(draggedGear);
        }
    }

    public virtual void FillSlot(Gear gear)
    {
        //Isso poderia ser um método abstract mas eu não tenho 100% de certeza
    }
}
