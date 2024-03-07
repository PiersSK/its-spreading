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

    protected override void Update()
    {
        base.Update();
        if(!hasWatched
            && (TimeController.Instance.IsInTimeSpan(hourOfBroadcastStart, minOfBroadcastStart, hourOfBroadcastEnd, minOfBroadcastEnd)
            && sofa.isLockedIn))
        {
            hasWatched = true;
            ThoughtBubble.Instance.ShowThought(PlayerThoughts.WatchingNews);
            Invoke(nameof(EventEnd), 5f);
        }
    }

    private void EventEnd()
    {
        hasReacted = true;
    }


    protected override bool ShouldEventTrigger()
    {
        return (hasReacted && !eventComplete);
    }

    protected override void EventImpact()
    {
        base.EventImpact();
    }
}
