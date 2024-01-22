using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missionary : NPC
{
    [SerializeField] private Door _door;

    [SerializeField] private NeighbourAppearance currentScheduler;

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
        //currentScheduler.TriggerEventEnd();
        Player.Instance.TogglePlayerIsEngaged();
    }

    public override bool CanInteract()
    {
        return _door.isOpen;
    }
}
