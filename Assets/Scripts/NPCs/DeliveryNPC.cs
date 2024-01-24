using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryNPC : NPC
{
    [SerializeField] private Door _door;

    private string objectToDeliverName;
    private InventoryItemData objectToDeliver;
    public InventoryItemData ObjectToDeliver
    {
        get { return objectToDeliver; }
        set
        { 
            objectToDeliver = value;
            objectToDeliverName = objectToDeliver.displayName;
            promptText = "Take " + objectToDeliverName;
        }
    }
    public NeighbourAppearance currentScheduler;

    public bool hasDelivered = false;

    public override void Interact()
    {
        Debug.Log("Gave " + objectToDeliverName + " to player");
        Player.Instance.GetComponent<InventorySystem>().AddItem(objectToDeliver);
        hasDelivered = true;
        currentScheduler.TriggerEventEnd();
    }

    public override bool CanInteract()
    {
        return !hasDelivered && _door.isOpen;
    }
}
