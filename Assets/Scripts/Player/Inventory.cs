using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // TODO: Make this a real system

    public enum InventoryItem
    {
        SuperPlantFormula,
        Cookies,
        DarrenPamphlet
    }

    public List<string> inventoryItemNames = new()
    {
        "Super Plant Formula",
        "Cookies",
        "Darren Pamphlet"
    };

    public List<InventoryItem> inventory = new();


}
