using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionaryResponse : DialogueResponse
{
    [SerializeField] public MissionaryResponse() { }

    [SerializeField] private InventoryItemData cookies;
    [SerializeField] private InventoryItemData religiousPamphlet;

    public void LeaveWithCookies()
    {
        Player.Instance.inventory.AddItem(cookies);
        CloseDialogue();
    }

    public void LeaveWithPamphlet()
    {
        Player.Instance.inventory.AddItem(religiousPamphlet);
        CloseDialogue();
    }

    public override void CloseDialogue()
    {
        Player.Instance.TogglePlayerIsEngaged();
        DialogueUI.Instance.currentNPC.currentScheduler.TriggerEventEnd();
        base.CloseDialogue();
    }
}
