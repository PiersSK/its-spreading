using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedEvent : MonoBehaviour 
{
    [Range(0,23)]
    [SerializeField] private int eventHour;
    [Range(0, 59)]
    [SerializeField] private int eventMinute;

    public bool hasBeenTriggered = false;

    public virtual void TriggerEvent() { }
    public virtual bool ShouldEventTrigger() {
        return !hasBeenTriggered && TimeController.Instance.TimeHasPassed(eventHour, eventMinute);
    }
}
