using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionaryResponse : DialogueResponse
{
    public MissionaryResponse() { }

    public void LeaveWithCookies()
    {
        Player.Instance.GetComponent<Inventory>().inventory.Add(Inventory.InventoryItem.Cookies);
        CloseDialogue();
    }

    public void LeaveWithPamphlet()
    {
        Player.Instance.GetComponent<Inventory>().inventory.Add(Inventory.InventoryItem.DarrenPamphlet);
        CloseDialogue();
    }
}
