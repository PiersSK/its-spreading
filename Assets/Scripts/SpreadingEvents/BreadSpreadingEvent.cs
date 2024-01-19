using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

public class BreadSpreadingEvent : SpreadingEvent
{
    [SerializeField] private Bread bread;

    [SerializeField] private float breadSpreadTimeRequired = 10f;
    [SerializeField] private float breadSpreadTime = 0f;

    protected override void Update()
    {
        base.Update();

        if (bread.isSpreading && !eventComplete)
        {
            breadSpreadTime += Time.deltaTime;
        }
    }

    protected override bool ShouldEventTrigger()
    {
        return breadSpreadTime <= breadSpreadTimeRequired && bread.isSpreading;
    }

    protected override void EventImpact()
    {
        base.EventImpact();
    }
}
