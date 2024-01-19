using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadingEvent : MonoBehaviour
{
    [SerializeField] Objective _objective;
    protected bool eventComplete = false;

    protected virtual void Update()
    {
        if (ShouldEventTrigger() && !eventComplete)
        {
            eventComplete = true;
            Confetti.Instance.ConfettiExplosion();
            _objective.CompleteObjective();
            ObjectiveController.Instance.ObjectivesComplete++;
            EventImpact();
        }
    }

    protected virtual bool ShouldEventTrigger()
    {
        return false;
    }

    protected virtual void EventImpact() { }

}
