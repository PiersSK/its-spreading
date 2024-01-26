using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedEvent : MonoBehaviour 
{
    [Range(0,23)]
    public int eventHour;
    [Range(0, 59)]
    public int eventMinute;

    public bool hasBeenTriggered = false;

    public virtual void TriggerEvent() { }
    public virtual bool ShouldEventTrigger() {
        return !hasBeenTriggered && TimeController.Instance.TimeHasPassed(eventHour, eventMinute);
    }

    public void SetEventStartTime(int hour, int min)
    {
        eventHour = hour;
        eventMinute = min;
    }
}
