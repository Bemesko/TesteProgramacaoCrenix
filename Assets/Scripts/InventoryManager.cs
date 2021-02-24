using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField]
    private InventorySlot[] _inventorySlots;

    public static InventoryManager Instance;

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

        for (int i = 0; i < _inventorySlots.Length; i++)
        {
            _inventorySlots[i]._slotID = i;
        }
    }
}
