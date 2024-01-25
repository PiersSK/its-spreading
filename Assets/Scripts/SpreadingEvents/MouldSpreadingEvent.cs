using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouldSpreadingEvent : SpreadingEvent
{
    [SerializeField] private Fridge _fridge;
    [SerializeField] private int rottingTimeSpan = 1;

    private string mouldSpreadThought;

    protected override bool ShouldEventTrigger()
    {
        return _fridge.petriInFridge && TimeController.Instance.TimeHasPassed(_fridge.petriInFridgeTime.Hours + rottingTimeSpan, _fridge.petriInFridgeTime.Minutes);
    }

    protected override void EventImpact()
    {
        mouldSpreadThought = Player.Instance.playerBelievesInDarren ? "Oh Magic Darren what's that smell?!" : "Oh God what's that smell?!";
        ThoughtBubble.Instance.ShowThought(mouldSpreadThought);
        _fridge.SetParticleEmission();
        base.EventImpact();
    }
}
