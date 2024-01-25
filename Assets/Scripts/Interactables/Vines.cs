using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vines : Interactable
{
    [SerializeField] private string requiredItem;

    [Range(0,120)]
    [SerializeField] private int coolDownInGameMins;
    private float cooldownTimer = 0f;
    private bool onCooldown;

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

    private const string EXAMINETHOUGHT = "These vines used to be spread all over the bathroom, now look at them...";
    private const string FIRSTWATERTHOUGHT = "Well, I'll give you some more in a while, hang in there.";

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        promptText = EXAMINEPROMPT;
    }

    private void Update()
    {
        if(onCooldown)
        {
            cooldownTimer += Time.deltaTime;
            if(cooldownTimer >= TimeController.Instance.InGameMinsToRealSeconds(coolDownInGameMins))
                onCooldown = false;
        }

        if(promptText != WATERPROMPT && Player.Instance.newInventory.HasItem(requiredItem))
        {
            promptText = WATERPROMPT;
        }
    }

    public override void Interact()
    {
        if (currentState == PlantState.Dead && !Player.Instance.newInventory.HasItem(requiredItem))
        {
            ThoughtBubble.Instance.ShowThought(EXAMINETHOUGHT);
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
            ThoughtBubble.Instance.ShowThought(FIRSTWATERTHOUGHT);
        }
        _animator.SetInteger("plantGrowthIndex", (int)currentState);

        if(currentState == PlantState.Overgrown)
        {
            Player.Instance.newInventory.RemoveItem(requiredItem);
        }

        onCooldown = true;
        cooldownTimer = 0f;
    }

    public override bool CanInteract()
    {
        return (Player.Instance.newInventory.HasItem(requiredItem) || !hasExamined)
            && currentState != PlantState.Overgrown
            && !onCooldown;
    }
}
