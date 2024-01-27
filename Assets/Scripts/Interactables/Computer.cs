using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : LockinInteractable
{
    [SerializeField] private AudioClip keyboardSound;
    protected override void FreeInteract()
    {
        GetComponent<AudioSource>().PlayOneShot(keyboardSound);
        ComputerUI.Instance.gameObject.SetActive(true);
        base.FreeInteract();
    }

    protected override void LockedInInteract()
    {
        ComputerUI.Instance.gameObject.SetActive(false);
        base.LockedInInteract();

    }

    public void ExitComputerManually()
    {
        ComputerUI.Instance.gameObject.SetActive(false);

        Player.Instance.transform.position = resetPosition;
        Player.Instance.transform.rotation = resetRotation;

        Player.Instance.TogglePlayerIsEngaged();
        Player.Instance.GetComponent<PlayerInteract>().persistSelectedInteractable = false;
        promptText = freePrompt;

        isLockedIn = false;
    }
}
