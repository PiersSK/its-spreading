using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vines : Interactable, IDataPersistence
{
    [SerializeField] private string requiredItem;

    [Range(0,120)]
    [SerializeField] private int coolDownInGameMins;
    private bool onCooldown;
    private TimeSpan onCooldownUntil;

    private bool hasExamined = false;

    [SerializeField] private ParticleSystem sparkle;
    [SerializeField] private AudioClip sparkleSound;

    [SerializeField] private Material aliveMaterial;
    [SerializeField] private List<Renderer> startingLeaves;

    public enum PlantState
    {
        Dead,
        Alive,
        Thriving,
        Overgrown
    }

    public PlantState currentState = PlantState.Dead;

    private Animator _animator;
    private AudioSource _audioSource;
    private const string EXAMINEPROMPT = "Examine";
    private const string WATERPROMPT = "Try Super Plant Formula";

    public void LoadData(GameData data)
    {
        currentState = data.plantState;
    }

    public void SaveData(ref GameData data)
    {
        data.plantState = currentState;
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        if (1 <= (int)currentState) 
            foreach (Renderer renderer in startingLeaves) renderer.material = aliveMaterial;
        _animator.SetInteger("plantGrowthIndex", (int)currentState);
        if(currentState == PlantState.Overgrown)
            _animator.Play("plantOvergrown");
        promptText = EXAMINEPROMPT;
    }

    private void Update()
    {
        if(onCooldown && TimeController.Instance.TimeHasPassed(onCooldownUntil)) onCooldown = false;

        if(promptText != WATERPROMPT && Player.Instance.newInventory.HasItem(requiredItem))
        {
            promptText = WATERPROMPT;
        }
    }

    public override void Interact()
    {
        if (currentState == PlantState.Dead && !Player.Instance.newInventory.HasItem(requiredItem))
        {
            ThoughtBubble.Instance.ShowThought(PlayerThoughts.InspectDeadVines);
            hasExamined = true;
            promptText = WATERPROMPT;
            return;
        }

        sparkle.Play();
        _audioSource.PlayOneShot(sparkleSound);
        currentState++;
        if (currentState == PlantState.Alive)
        {
            foreach (Renderer renderer in startingLeaves) renderer.material = aliveMaterial;
            ThoughtBubble.Instance.ShowThought(PlayerThoughts.WateredVinesOnce);
        }
        _animator.SetInteger("plantGrowthIndex", (int)currentState);

        if(currentState == PlantState.Overgrown)
        {
            Player.Instance.newInventory.RemoveItem(requiredItem);
        }

        PutOnCooldown();
    }

    private void PutOnCooldown()
    {
        onCooldown = true;
        TimeSpan now = TimeController.Instance.CurrentTime();
        onCooldownUntil = now + TimeSpan.FromMinutes(coolDownInGameMins);
        Debug.Log("On cooldown until " + onCooldownUntil);
    }

    public override bool CanInteract()
    {
        return (Player.Instance.newInventory.HasItem(requiredItem) || !hasExamined)
            && currentState != PlantState.Overgrown
            && !onCooldown;
    }
}
