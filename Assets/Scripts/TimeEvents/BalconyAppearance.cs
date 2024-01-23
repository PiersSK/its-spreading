using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalconyAppearance : NeighbourAppearance
{
    protected override void OnComplete()
    {
        base.OnComplete();
        neighbour.GetComponent<DownstairsNeighbour>()._animator.SetBool("isLeaning", true);
    }

    public override void TriggerEventEnd()
    {
        base.TriggerEventEnd();
        neighbour.GetComponent<DownstairsNeighbour>()._animator.SetBool("isLeaning", false);
    }
}
