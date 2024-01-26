using UnityEngine;

public class TvBroadcast : LimitedTimedEvent
{
    [SerializeField] public Light tvLight;
    [SerializeField] public Color originalColour;
    [SerializeField] public Color newColour;
    public override void TriggerEvent()
    {
        tvLight.color = newColour;
    }

    public override void TriggerEventEnd()
    {
        tvLight.color = originalColour;
    }
}