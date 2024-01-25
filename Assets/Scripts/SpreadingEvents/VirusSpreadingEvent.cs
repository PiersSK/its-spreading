using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class VirusSpreadingEvent : SpreadingEvent
{
    [SerializeField] private Seat sofa;
    [SerializeField] private int hourOfBroadcastStart = 18;
    [SerializeField] private int minOfBroadcastStart = 0;
    [SerializeField] private int hourOfBroadcastEnd = 18;
    [SerializeField] private int minOfBroadcastEnd = 30;

    private bool hasWatched = false;
    private bool hasReacted = false;

    private const string INITIALTHOUGHT = "Oh spaghettios, they're considering a lockdown in response to the spreading of the gigglepox pandemic.";

    protected override void Update()
    {
        base.Update();
        if(!hasWatched
            && (TimeController.Instance.IsInTimeSpan(hourOfBroadcastStart, minOfBroadcastStart, hourOfBroadcastEnd, minOfBroadcastEnd)
            && sofa.isLockedIn))
        {
            hasWatched = true;
            ThoughtBubble.Instance.ShowThought(INITIALTHOUGHT);
            Invoke(nameof(EventEnd), 5f);
        }
    }

    private void EventEnd()
    {
        hasReacted = true;
    }


    protected override bool ShouldEventTrigger()
    {
        return hasReacted;
    }
}
