using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missionary : NPC
{
    [SerializeField] private Door _door;
    [SerializeField] private TextAsset dialogueFile;


    protected override void Start()
    {
        base.Start();
        currentScheduler.NPCReachedDestination += CurrentScheduler_NPCReachedDestination;
    }

    private void CurrentScheduler_NPCReachedDestination(object sender, System.EventArgs e)
    {
        _door.KnockAtDoor();
    }

    public override void Interact()
    {
        Player.Instance.TogglePlayerIsEngaged();
        DialogueUI.Instance.LoadJsonConversationToUI(dialogueFile, this);
        DialogueUI.Instance.gameObject.SetActive(true);
    }

    public override bool CanInteract()
    {
        return _door.isOpen;
    }
}
