using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vines : Interactable
{
    [SerializeField] private Inventory.InventoryItem requiredItem;

    [SerializeField] private ParticleSystem sparkle;
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
    private void Start()
    {
        _animator = GetComponent<Animator>(); 
    }

    public override void Interact()
    {
        sparkle.Play();
        currentState++;
        if (currentState == PlantState.Alive)
            foreach (Renderer renderer in startingLeaves) renderer.material = aliveMaterial;
        _animator.SetInteger("plantGrowthIndex", (int)currentState);
    }

    public override bool CanInteract()
    {
        return Player.Instance.GetComponent<Inventory>().inventory.Contains(requiredItem) && currentState != PlantState.Overgrown;
    }
}
