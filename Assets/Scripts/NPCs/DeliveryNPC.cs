using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryNPC : NPC
{
    [SerializeField] private Door _door;

    private string objectToDeliver;
    public string ObjectToDeliver
    {
        get { return objectToDeliver; }
        set
        { 
            objectToDeliver = value;
            promptText = "Take " + value;
        }
    }
    public NeighbourAppearance currentScheduler;

    public bool hasDelivered = false;

    public override void Interact()
    {
        Debug.Log("Gave " + ObjectToDeliver + " to player");
        hasDelivered = true;
        currentScheduler.TriggerEventEnd();
    }

    public override bool CanInteract()
    {
        return !hasDelivered && _door.isOpen;
    }
}
