using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryResponse : DialogueResponse
{
    public override void CloseDialogue()
    {
        Player.Instance.TogglePlayerIsEngaged();
        DialogueUI.Instance.currentNPC.currentScheduler.TriggerEventEnd();
        base.CloseDialogue();
    }

    public void LeaveWithPackage()
    {
        ReceivePackage();
        CloseDialogue();
    }

    public void ReceivePackage()
    {
        DialogueUI.Instance.currentNPC.NPCCoreAction(); // Assumed NPC is of type DeliveryNPC. How to check this?
    }

    public void RemovePamphlet()
    {
        Player.Instance._inventory.RemoveFromInventory(Inventory.InventoryItem.DarrenPamphlet);
    }

    public bool HasPamphlet()
    {
        return Player.Instance._inventory.IsItemInInventory(Inventory.InventoryItem.DarrenPamphlet);
    }
}
