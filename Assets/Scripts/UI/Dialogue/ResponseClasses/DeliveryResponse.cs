using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryResponse : DialogueResponse
{
    [SerializeField] private InventoryItemData itemToReceive;
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
        Player.Instance.inventory.RemoveItem(itemToReceive);
    }

    public bool HasPamphlet()
    {
        return Player.Instance.inventory.HasItem(itemToReceive);
    }
}
