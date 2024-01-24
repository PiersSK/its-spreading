using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public List<InventoryItemData> inventory { get; private set; }

    public delegate void inventoryChangeDelegate();
    public inventoryChangeDelegate onInventoryChangeEvent;

    public void Awake()
    {
        inventory = new List<InventoryItemData>();
    }

    public void AddItem(InventoryItemData itemData)
    {
        if(!inventory.Contains(itemData))
        {
            inventory.Add(itemData);
            if (onInventoryChangeEvent != null) onInventoryChangeEvent();
        }
    }

    public void RemoveItem(InventoryItemData itemData)
    {
        if (inventory.Contains(itemData))
        {
            inventory.Remove(itemData);
            if (onInventoryChangeEvent != null) onInventoryChangeEvent();
        }
    }

    public bool HasItem(InventoryItemData itemData)
    {
        return inventory.Contains(itemData);
    }

}
