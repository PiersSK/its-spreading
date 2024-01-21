using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpreadingEvent : SpreadingEvent
{
    [SerializeField] Fire spreadFire;

    protected override bool ShouldEventTrigger()
    {
        return spreadFire.isLit;
    }
}
