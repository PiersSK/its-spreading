using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouldSpreadingEvent : SpreadingEvent
{
    [SerializeField] private Fridge _fridge;

    private string mouldSpreadThought;

    protected override bool ShouldEventTrigger()
    {
        return _fridge.petriInFridge && _fridge.postRottenInteract;
    }

    protected override void EventImpact()
    {
        mouldSpreadThought = Player.Instance.playerBelievesInDarren ? "Oh Magic Darren what's that smell?!" : "Oh God what's that smell?!";
        ThoughtBubble.Instance.ShowThought(mouldSpreadThought);
        _fridge.SetParticleEmission();
        _fridge.reminderSent = true;
        base.EventImpact();
    }
}
