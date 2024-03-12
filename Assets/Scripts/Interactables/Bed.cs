using UnityEngine;

public class Bed : LockinInteractable
{
    [SerializeField] private float restTimeMultiplier;

    protected override void FreeInteract()
    {
        if (!ObjectiveController.HasCompletedAllObjectives())
        {
            base.FreeInteract();
            TimeController.Instance.tempMultiplier = restTimeMultiplier;
        } else
        {
            EndGame.Instance.DayIsOver();
        }
    }

    protected override void LockedInInteract()
    {
        base.LockedInInteract();
        TimeController.Instance.tempMultiplier = 1f;
    }
}
