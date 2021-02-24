using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PZGear : MonoBehaviour
{
    private Vector3 initialMousePoisition;
    private bool _isBeingHeld = false;
    private Camera _mainCamera;

    private void Awake()
    {
        //Fazendo isso porque Camera.main é ruim pra performance
        _mainCamera = Camera.main;
    }

    void Update()
    {
        Vector3 currentMousePosition = Input.mousePosition;
        currentMousePosition = _mainCamera.ScreenToWorldPoint(currentMousePosition);

        if (_isBeingHeld)
        {
            transform.localPosition = new Vector3(currentMousePosition.x - initialMousePoisition.x, currentMousePosition.y - initialMousePoisition.y, transform.localPosition.z);
        }
    }

    private void OnMouseDown()
    {

        Vector3 mousePosition = Input.mousePosition;
        mousePosition = _mainCamera.ScreenToWorldPoint(mousePosition);

        float newStartPositionX = mousePosition.x - transform.localPosition.x;
        float newStartPositionY = mousePosition.y - transform.localPosition.y;
        initialMousePoisition = new Vector3(newStartPositionX, newStartPositionY, mousePosition.z);


        _isBeingHeld = true;

    }

    private void OnMouseUp()
    {
        _isBeingHeld = false;
    }
}
