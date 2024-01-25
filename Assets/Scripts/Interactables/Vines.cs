using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vines : Interactable, IDataPersistence
{
    [SerializeField] private Inventory.InventoryItem requiredItem;

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

        onCooldown = true;
        cooldownTimer = 0f;
    }

    public override bool CanInteract()
    {
        return Player.Instance._inventory.IsItemInInventory(requiredItem)
            && currentState != PlantState.Overgrown
            && !onCooldown;
    }
}
