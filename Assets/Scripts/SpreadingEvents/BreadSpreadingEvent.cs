using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

public class BreadSpreadingEvent : SpreadingEvent
{
    [SerializeField] private Bread bread;

    [SerializeField] private int breadSpreadTimeRequired = 10;

    protected override void Update()
    {
        base.Update();
    }

    protected override bool ShouldEventTrigger()
    {
        return !TimeController.Instance.TimeHasPassed(breadSpreadTimeRequired, 0) && bread.isSpreading;
    }

    protected override void EventImpact()
    {
        base.EventImpact();
    }
}