using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour, IDataPersistence
{
    public event EventHandler<OnInventoryChangedEventArgs> OnInventoryChanged;
    public class OnInventoryChangedEventArgs: EventArgs
    {
        public InventoryItem itemAdded;
        public InventoryItem itemRemoved;
    }

    public enum InventoryItem
    {
        SuperPlantFormula,
        Cookies,
        DarrenPamphlet,

        None
    }

    private List<string> inventoryItemNames = new()
    {
        "Super Plant Formula",
        "Cookies",
        "Darren Pamphlet"
    };

    [SerializeField] private List<InventoryItem> inventory = new();

    public void LoadData(GameData data)
    {
        inventory = data.inventory;
    }

    public void SaveData(ref GameData data)
    {
        data.inventory = inventory;
    }

    public void AddToInventory(InventoryItem item)
    {
        inventory.Add(item);
        OnInventoryChanged?.Invoke(this, new OnInventoryChangedEventArgs() { itemAdded = item, itemRemoved = InventoryItem.None});
        Debug.Log(inventory);
    }

    public void RemoveFromInventory(InventoryItem item)
    {
        inventory.Remove(item);
        OnInventoryChanged?.Invoke(this, new OnInventoryChangedEventArgs() { itemAdded = InventoryItem.None, itemRemoved = item});
    }

    public string GetItemName(InventoryItem item)
    {
        return inventoryItemNames[(int)item];
    }

    public bool IsItemInInventory(InventoryItem item) {
        return inventory.Contains(item);
    }
}
