using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fridge : Interactable
{

    [SerializeField] bool petriInFridge = false;
    [SerializeField] bool petriRotten = false;
    [SerializeField] int rottenWaitTime = 3;

    public override void Interact()
    {
        base.Interact();
        if(!petriInFridge)
            Player.Instance.newInventory.RemoveItem("petriDish");
    }

    public override bool CanInteract()
    {
        return Player.Instance.newInventory.HasItem("petriDish");
    }
}
