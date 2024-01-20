using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class Seat : LockinInteractable
{
    public event EventHandler<EventArgs> PlayerHasSat;
    public event EventHandler<EventArgs> PlayerHasStood;

    protected override void FreeInteract()
    {
        Player.Instance.GetComponent<Animator>().SetBool("isSitting", true);
        PlayerHasSat?.Invoke(this, new EventArgs());
        base.FreeInteract();
    }

    protected override void LockedInInteract()
    {
        Player.Instance.GetComponent<Animator>().SetBool("isSitting", false);
        PlayerHasStood?.Invoke(this, new EventArgs());
        base.LockedInInteract();

    }
}
