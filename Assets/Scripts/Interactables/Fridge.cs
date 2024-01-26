using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fridge : Interactable
{

    [SerializeField] public bool petriInFridge = false;
    [SerializeField] public bool postRottenInteract = false;
    public bool reminderSent = false;

    [SerializeField] private int rottingTimeSpan = 1;
    [SerializeField] private int checkOnFriendReminderminutes = 30;

    [SerializeField] public ParticleSystem stinkParticles;

    [SerializeField] private string checkFriendReminder = "I wonder how my petri friend is doing?";

    public TimeSpan petriInFridgeTime;

    public void Awake()
    {
        petriInFridgeTime = new TimeSpan();
    }

    public void Update()
    {
        if (IsFriendRotten() && TimeController.Instance.TimeHasPassed(petriInFridgeTime.Hours + rottingTimeSpan, petriInFridgeTime.Minutes + checkOnFriendReminderminutes))
        {
            if (!reminderSent)
            {
                ThoughtBubble.Instance.ShowThought(checkFriendReminder);
                reminderSent = true;
            }
        }
    }

    public override void Interact()
    {
        base.Interact();
        if(!petriInFridge)
        {
            Player.Instance.newInventory.RemoveItem("petriDish");
            petriInFridgeTime = TimeController.Instance.CurrentTime();
            petriInFridge = true;
        } 
        else if (IsFriendRotten())
        {
            postRottenInteract = true;
        }
    }

    public override bool CanInteract()
    {
        return Player.Instance.newInventory.HasItem("petriDish") || IsFriendRotten();
    }

    public void SetParticleEmission()
    {
        var emission = stinkParticles.emission;
        emission.rateOverTime = 12;
        stinkParticles.Play();
    }

    private bool IsFriendRotten()
    {
       return petriInFridge && TimeController.Instance.TimeHasPassed(petriInFridgeTime.Hours + rottingTimeSpan, petriInFridgeTime.Minutes);
    }
}
