using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionaryResponse : DialogueResponse
{
    public MissionaryResponse() { }
    [SerializeField] private InventoryItemData cookies;
    [SerializeField] private InventoryItemData darrenPamphlet;

    public void LeaveWithCookies()
    {
        Player.Instance.newInventory.AddItem(cookies);
        CloseDialogue();
    }

    public void LeaveWithPamphlet()
    {
        Player.Instance.newInventory.AddItem(darrenPamphlet);
        CloseDialogue();
    }

    public override void CloseDialogue()
    {
        Player.Instance.TogglePlayerIsEngaged();
        DialogueUI.Instance.currentNPC.currentScheduler.TriggerEventEnd();
        base.CloseDialogue();
    }
}
