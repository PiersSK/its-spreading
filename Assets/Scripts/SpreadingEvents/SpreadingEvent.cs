using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadingEvent : MonoBehaviour
{
    protected bool eventComplete = false;

    protected virtual void Update()
    {
        if (ShouldEventTrigger() && !eventComplete)
        {
            Confetti.Instance.ConfettiExplosion();
            eventComplete = true;
        }
    }

    protected virtual bool ShouldEventTrigger()
    {
        return false;
    }

}
