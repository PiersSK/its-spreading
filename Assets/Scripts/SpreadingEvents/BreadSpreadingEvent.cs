using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.WSA;

public class BreadSpreadingEvent : SpreadingEvent
{
    [SerializeField] private Bread bread;

    protected override void Update()
    {
        base.Update();
    }

    protected override bool ShouldEventTrigger()
    {
        return !TimeController.Instance.TimeHasPassed(10, 0) && bread.isSpreading;
    }

    protected override void EventImpact()
    {
        base.EventImpact();
    }
}
