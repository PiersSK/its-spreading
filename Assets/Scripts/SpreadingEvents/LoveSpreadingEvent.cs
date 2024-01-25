using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoveSpreadingEvent : SpreadingEvent
{
    public static LoveSpreadingEvent Instance { get; private set; }

    public bool calledSis = false;
    public bool bookedTickets = false;
    public bool gaveTicketsToSis = false;

    private void Awake()
    {
        Instance = this;
    }

    protected override bool ShouldEventTrigger()
    {
        return gaveTicketsToSis;
    }
}
