using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missionary : DialogueNPC
{
    [SerializeField] private Door _door;


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
        base.Interact();
        GetComponent<Animator>().SetTrigger("adjustGlasses");
    }

    public override bool CanInteract()
    {
        return _door.isOpen && !currentScheduler.npcIsEngaged;
    }
}
