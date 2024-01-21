using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VineSpreadingEvent : SpreadingEvent
{
    [SerializeField] private Vines _vines;

    protected override bool ShouldEventTrigger()
    {
        return _vines.currentState == Vines.PlantState.Overgrown;
    }
}
