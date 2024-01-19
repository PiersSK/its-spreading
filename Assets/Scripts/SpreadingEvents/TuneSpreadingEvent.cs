using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TuneSpreadingEvent : SpreadingEvent
{
    [SerializeField] private Piano piano;
    [SerializeField] private NeighbourAppearance neighbourAppearance;

    [SerializeField] private float tuneSpreadTimeRequired = 12f;
    [SerializeField] private float tuneSpreadTime = 0f;

    protected override void Update()
    {
        base.Update(); // Needed to keep event check
        if(piano.isPlaying && neighbourAppearance.neighbourIsOut && !eventComplete)
        {
            tuneSpreadTime += Time.deltaTime;
        }
    }
    protected override bool ShouldEventTrigger()
    {
        return tuneSpreadTime >= tuneSpreadTimeRequired;
    }

}
