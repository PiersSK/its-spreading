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
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if(onCooldown)
        {
            cooldownTimer += Time.deltaTime;
            if(cooldownTimer >= TimeController.Instance.InGameMinsToRealSeconds(coolDownInGameMins))
                onCooldown = false;
        }
    }

    public override void Interact()
    {
        sparkle.Play();
        _audioSource.PlayOneShot(sparkleSound);
        currentState++;
        if (currentState == PlantState.Alive)
            foreach (Renderer renderer in startingLeaves) renderer.material = aliveMaterial;
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
        return Player.Instance.newInventory.HasItem(requiredItem)
            && currentState != PlantState.Overgrown
            && !onCooldown;
    }
}
