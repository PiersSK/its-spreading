using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitedTimedEvent : TimedEvent
{
    [Range(0, 23)]
    [SerializeField] protected int eventEndHour;
    [Range(0, 59)]
    [SerializeField] protected int eventEndMinute;

    public bool eventHasEnded = false;

    public virtual void TriggerEventEnd() { }
    public virtual bool ShouldEventEndTrigger()
    {
        return hasBeenTriggered && !eventHasEnded && TimeController.Instance.TimeHasPassed(eventEndHour, eventEndMinute);
    }

    public void SetEventEndTime(int hour, int min)
    {
        eventEndHour = hour;
        eventEndMinute = min;
    }
}
