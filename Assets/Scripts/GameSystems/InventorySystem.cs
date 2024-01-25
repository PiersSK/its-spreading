using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Inventory;

public class InventorySystem : MonoBehaviour
{
    [SerializeField] public List<InventoryItemData> availableItems;
    public Dictionary<string, InventoryItemData> availableItemDict { get;  private set; }

    public Dictionary<string, InventoryItemData> inventory { get; private set; }

    public event EventHandler<OnInventoryChangedEventArgs> OnInventoryChanged;
    public class OnInventoryChangedEventArgs : EventArgs
    {
        public string itemAdded;
        public string itemRemoved;
    }

    public void Awake()
    {
        availableItemDict = availableItems.ToDictionary(x => x.id, x => x);
        foreach (string item in availableItemDict.Keys)
        {
            Debug.Log($"available item contains: {item}");
        }
        inventory = new Dictionary<string, InventoryItemData>();
    }

    public void AddItem(string itemName)
    {
        if (!inventory.ContainsValue(availableItemDict[itemName]))
        {
            inventory.Add(itemName, availableItemDict[itemName]);
            Debug.Log($"{itemName} added to inventory");
            if (OnInventoryChanged != null) OnInventoryChanged.Invoke(this, new OnInventoryChangedEventArgs() { itemAdded = itemName, itemRemoved = ""});
        }
    }

    public void RemoveItem(string itemName)
    {
        if (inventory.ContainsValue(availableItemDict[itemName]))
        {
            inventory.Remove(itemName);
            if (OnInventoryChanged != null) OnInventoryChanged.Invoke(this, new OnInventoryChangedEventArgs() { itemAdded = "", itemRemoved = itemName });
        }
    }

    public bool HasItem(InventoryItemData itemData)
    {
        return inventory.ContainsValue(itemData);
    }
    
    public bool HasItem(string itemName)
    {
        foreach(string item in inventory.Keys)
        {
            Debug.Log($"{item} in inventory");
        }
        Debug.Log($"Items in inventory: {inventory.Keys}");
        return inventory.ContainsKey(itemName);
    }

}
