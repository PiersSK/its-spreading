using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpreadingEvent : SpreadingEvent
{
    [SerializeField] private Fire firstFire;
    [SerializeField] private Fire spreadFire;
    [SerializeField] private Room kitchen;

    private bool hasReacted = false;

    private const string FIRSTFIRETHOUGHT = "It dances so beautifully.";

    protected override void Update()
    {
        base.Update();
        if(firstFire.isLit && !spreadFire.isLit && !hasReacted && Player.Instance.currentRoom == kitchen)
        {
            ThoughtBubble.Instance.ShowThought(FIRSTFIRETHOUGHT);
            hasReacted = true;
        }
    }
    protected override bool ShouldEventTrigger()
    {
        return spreadFire.isLit;
    }
}
