using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Inventory;

public class InventorySystem : MonoBehaviour
{
    public List<InventoryItemData> inventory { get; private set; }

    public event EventHandler<OnInventoryChangedEventArgs> OnInventoryChanged;
    public class OnInventoryChangedEventArgs : EventArgs
    {
        public InventoryItemData itemAdded;
        public InventoryItemData itemRemoved;
    }

    public void Awake()
    {
        inventory = new List<InventoryItemData>();
    }

    public void AddItem(InventoryItemData itemData)
    {
        if (!inventory.Contains(itemData))
        {
            inventory.Add(itemData);
            if (OnInventoryChanged != null) OnInventoryChanged.Invoke(this, new OnInventoryChangedEventArgs() { itemAdded = itemData, itemRemoved = new InventoryItemData() });
        }
    }

    public void RemoveItem(InventoryItemData itemData)
    {
        if (inventory.Contains(itemData))
        {
            inventory.Remove(itemData);
            if (OnInventoryChanged != null) OnInventoryChanged.Invoke(this, new OnInventoryChangedEventArgs() { itemAdded = new InventoryItemData(), itemRemoved = itemData });
        }
    }

    public bool HasItem(InventoryItemData itemData)
    {
        return inventory.Contains(itemData);
    }

}
