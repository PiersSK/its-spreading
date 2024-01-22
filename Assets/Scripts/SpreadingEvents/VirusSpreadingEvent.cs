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
    protected override void Update()
    {
        base.Update();

    }

    protected override bool ShouldEventTrigger()
    {
        return (TimeController.Instance.IsInTimeSpan(hourOfBroadcastStart, minOfBroadcastStart, hourOfBroadcastEnd, minOfBroadcastEnd) && sofa.isLockedIn);
    }
}
