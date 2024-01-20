using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockinInteractable : Interactable
{
    [SerializeField] protected Transform lockinPos;
    protected Vector3 resetPosition;
    protected Quaternion resetRotation;

    protected bool isLockedIn = false;
    [SerializeField] protected string freePrompt;
    [SerializeField] protected string lockedPrompt;

    public override void Interact()
    {
        if(isLockedIn)
        {
            LockedInInteract();
        } else
        {
            FreeInteract();
        }
    }

    protected virtual void LockedInInteract()
    {
        Player.Instance.transform.position = resetPosition;
        Player.Instance.transform.rotation = resetRotation;

        Player.Instance.TogglePlayerIsEngaged();
        Player.Instance.GetComponent<PlayerInteract>().persistSelectedInteractable = false;
        promptText = freePrompt;

        isLockedIn = false;
    }

    protected virtual void FreeInteract()
    {
        Player.Instance.GetComponent<PlayerInteract>().persistSelectedInteractable = true;
        Player.Instance.TogglePlayerIsEngaged();

        resetPosition = Player.Instance.transform.position;
        resetRotation = Player.Instance.transform.rotation;

        Player.Instance.transform.position = lockinPos.position;
        Player.Instance.transform.rotation = lockinPos.rotation;
        promptText = lockedPrompt;

        isLockedIn = true;
    }

}
