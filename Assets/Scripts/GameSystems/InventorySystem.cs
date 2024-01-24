using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public Dictionary<string, InventoryItemData> itemDict { get; private set; }

    public delegate void inventoryChangeDelegate();
    public inventoryChangeDelegate onInventoryChangeEvent;

    public void Awake()
    {
        itemDict = new Dictionary<String, InventoryItemData>();
    }

    public void AddItem(InventoryItemData itemData)
    {
        if(!itemDict.ContainsValue(itemData))
        {
            itemDict.Add(itemData.id, itemData);
            if (onInventoryChangeEvent != null) onInventoryChangeEvent();
        }
    }

    public void RemoveItem(InventoryItemData itemData)
    {
        if (itemDict.ContainsValue(itemData))
        {
            itemDict.Remove(itemData.id);
            if (onInventoryChangeEvent != null) onInventoryChangeEvent();
        }
    }

    public bool HasItem(InventoryItemData itemData)
    {
        return itemDict.ContainsValue(itemData);
    }

    public bool HasItem(string itemId)
    {
        return itemDict.ContainsKey(itemId);
    }

}
