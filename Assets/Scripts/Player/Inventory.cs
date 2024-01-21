using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // TODO: Make this a real system

    public enum InventoryItem
    {
        SuperPlantFormula
    }

    public List<string> inventoryItemNames = new()
    {
        "Super Plant Formula"
    };

    public List<InventoryItem> inventory = new();


}
