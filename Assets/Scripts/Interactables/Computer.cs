using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : LockinInteractable
{
    [SerializeField] private AudioClip keyboardSound;

    private void Update()
    {
        bool playerIsDancing = Player.Instance._animator.GetBool("isDancing");

        if (!ComputerUI.Instance.gameObject.activeSelf && isLockedIn && !playerIsDancing)
            ExitComputerManually(); 
    }

    protected override void FreeInteract()
    {
        GetComponent<AudioSource>().PlayOneShot(keyboardSound);
        ComputerUI.Instance.gameObject.SetActive(true);
        TimeController.Instance.ToggleTimePause();
        base.FreeInteract();
    }

    protected override void LockedInInteract()
    {
        ComputerUI.Instance.gameObject.SetActive(false);
        TimeController.Instance.ToggleTimePause();
        base.LockedInInteract();
    }

    private void ExitComputerManually()
    {
        Player.Instance.transform.position = resetPosition;
        Player.Instance.transform.rotation = resetRotation;

        Player.Instance.FreePlayerIfEngaged();
        Player.Instance.GetComponent<PlayerInteract>().persistSelectedInteractable = false;
        promptText = freePrompt;

        TimeController.Instance.ToggleTimePause();

        isLockedIn = false;
    }
}
