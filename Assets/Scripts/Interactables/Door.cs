using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    private Animator doorAnimation;
    private bool isOpen = false;

    private const string OPENPROMPT = "Open Door";
    private const string CLOSEPROMPT = "Close Door";

    private void Start()
    {
        doorAnimation = GetComponent<Animator>();
        promptText = OPENPROMPT;
    }

    public override void Interact()
    {
        isOpen = !isOpen;
        doorAnimation.SetBool("isOpen", isOpen);
        promptText = isOpen ? CLOSEPROMPT : OPENPROMPT;
    }
}
