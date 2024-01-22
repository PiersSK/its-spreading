using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueNPC : NPC
{
    [SerializeField] protected TextAsset dialogueFile;

    public override void Interact()
    {
        Player.Instance.TogglePlayerIsEngaged(true);
        currentScheduler.npcIsEngaged = true;
        DialogueUI.Instance.LoadJsonConversationToUI(dialogueFile, this);
        DialogueUI.Instance.gameObject.SetActive(true);
    }
}
