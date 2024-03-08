using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionaryResponse : DialogueResponse
{
    public MissionaryResponse() { }

    public void LeaveWithCookies()
    {
        Player.Instance.newInventory.AddItem("cookie");
        CloseDialogue();
    }

    public void LeaveWithPamphlet()
    {
        Player.Instance.newInventory.AddItem("pamphlet");
        CloseDialogue();
    }

    public override void CloseDialogue()
    {
        Player.Instance.FreePlayerIfEngaged();
        DialogueUI.Instance.currentNPC.currentScheduler.TriggerEventEnd();
        base.CloseDialogue();
    }
}
