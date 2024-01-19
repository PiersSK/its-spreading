using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TuneSpreadingEvent : SpreadingEvent
{
    [SerializeField] private Piano piano;
    [SerializeField] private NeighbourAppearance neighbourAppearance;
    [SerializeField] private NPC neighbourNPC;

    [SerializeField] private float tuneSpreadTimeRequired = 12f;
    [SerializeField] private float tuneSpreadTime = 0f;

    protected override void Update()
    {
        base.Update(); // Needed to keep event check

        if (piano.isPlaying && neighbourAppearance.neighbourIsOut && !eventComplete)
        {
            tuneSpreadTime += Time.deltaTime;
            if (!neighbourNPC.audioIsPlaying) { 
                neighbourNPC.SetTrackToHum();
                neighbourNPC.PlayNPCAudio();
            }
        } else if (!eventComplete)
        {
            if (neighbourNPC.audioIsPlaying)
            {
                neighbourNPC.PauseNPCAudio();
            }
        }
    }

    protected override bool ShouldEventTrigger()
    {
        return tuneSpreadTime >= tuneSpreadTimeRequired;
    }

    protected override void EventImpact()
    {
        neighbourNPC.SetTrackToWhistle();
        neighbourNPC.PlayNPCAudio();

    }

}
