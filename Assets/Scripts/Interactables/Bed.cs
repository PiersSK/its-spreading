using UnityEngine;

public class Bed : LockinInteractable
{
    [SerializeField] private float restTimeMultiplier;

    protected override void FreeInteract()
    {
        base.FreeInteract();
        TimeController.Instance.tempMultiplier = restTimeMultiplier;
    }

    protected override void LockedInInteract()
    {
        base.LockedInInteract();
        TimeController.Instance.tempMultiplier = 1f;
    }
}
