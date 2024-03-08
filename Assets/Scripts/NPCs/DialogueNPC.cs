using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueNPC : NPC
{
    public TextAsset dialogueFile;

    public override void Interact()
    {
        currentScheduler.npcIsEngaged = true;
        DialogueUI.Instance.StartConversation(dialogueFile, this);
    }
}
