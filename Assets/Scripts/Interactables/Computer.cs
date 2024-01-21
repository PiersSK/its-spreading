using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : LockinInteractable
{
    protected override void FreeInteract()
    {
        ComputerUI.Instance.gameObject.SetActive(true);
        base.FreeInteract();
    }

    protected override void LockedInInteract()
    {
        ComputerUI.Instance.gameObject.SetActive(false);
        base.LockedInInteract();

    }
}
