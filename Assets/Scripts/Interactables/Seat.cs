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
        base.FreeInteract();
        Player.Instance.GetComponent<Animator>().SetBool("isSitting", true);
        PlayerHasSat?.Invoke(this, new EventArgs());
    }

    protected override void LockedInInteract()
    {
        base.LockedInInteract();
        Player.Instance.GetComponent<Animator>().SetBool("isSitting", false);
        PlayerHasStood?.Invoke(this, new EventArgs());

    }
}
