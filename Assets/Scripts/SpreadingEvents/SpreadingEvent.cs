using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadingEvent : MonoBehaviour
{
    public event EventHandler<EventArgs> SpreadEventComplete;

    [SerializeField] Objective _objective;
    protected bool eventComplete = false;

    protected virtual void Update()
    {
        if (ShouldEventTrigger() && !eventComplete)
        {
            eventComplete = true;
            Confetti.Instance.ConfettiExplosion(_objective.spreadingVoiceLine);
            _objective.CompleteObjective();
            ObjectiveController.Instance.ObjectivesComplete++;
            EventImpact();
            SpreadEventComplete?.Invoke(this, EventArgs.Empty);
        }
    }

    protected virtual bool ShouldEventTrigger()
    {
        return false;
    }

    protected virtual void EventImpact() { }

}
