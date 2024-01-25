using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpreadingEvent : SpreadingEvent
{
    [SerializeField] Fire firstFire;
    [SerializeField] Fire spreadFire;

    private bool hasReacted = false;

    private const string FIRSTFIRETHOUGHT = "It dances so beautifully.";

    protected override void Update()
    {
        base.Update();
        if(firstFire.isLit && !hasReacted)
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
