using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Inventory;

public class InventorySystem : MonoBehaviour, IDataPersistence
{
    [SerializeField] public List<InventoryItemData> availableItems;
    public Dictionary<string, InventoryItemData> availableItemDict { get;  private set; }

    public Dictionary<string, InventoryItemData> inventory { get; private set; }

    public event EventHandler<OnInventoryChangedEventArgs> OnInventoryChanged;

    public List<string> invIds = new();
    public class OnInventoryChangedEventArgs : EventArgs
    {
        public string itemAdded;
        public string itemRemoved;
    }

    public void LoadData (GameData data)
    {
        foreach(string item in data.inventory)
        {
            AddItem(item);
        }
        
    }

    public void SaveData (ref GameData data)
    {
        data.inventory = invIds;
    }

    public void Awake()
    {
        availableItemDict = availableItems.ToDictionary(x => x.id, x => x);
        inventory = new Dictionary<string, InventoryItemData>();
    }

    public void AddItem(string itemName)
    {
        if (!inventory.ContainsValue(availableItemDict[itemName]))
        {
            inventory.Add(itemName, availableItemDict[itemName]);
            invIds.Add(itemName);
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
        return inventory.ContainsKey(itemName);
    }

}
