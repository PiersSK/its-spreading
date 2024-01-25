using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fridge : Interactable
{

    [SerializeField] public bool petriInFridge = false;
    [SerializeField] bool petriRotten = false;

    [SerializeField] public ParticleSystem stinkParticles;

    public TimeSpan petriInFridgeTime;

    public void Awake()
    {
        petriInFridgeTime = new TimeSpan();
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
    }

    public override bool CanInteract()
    {
        return Player.Instance.newInventory.HasItem("petriDish");
    }

    public void SetParticleEmission()
    {
        var emission = stinkParticles.emission;
        emission.rateOverTime = 12;
        stinkParticles.Play();
    }
}
