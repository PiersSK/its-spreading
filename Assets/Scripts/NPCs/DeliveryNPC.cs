using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryNPC : NPC
{
    [SerializeField] private Door _door;

    private string objectToDeliverName;
    private Inventory.InventoryItem objectToDeliver;
    public Inventory.InventoryItem ObjectToDeliver
    {
        get { return objectToDeliver; }
        set
        { 
            objectToDeliver = value;
            objectToDeliverName = Player.Instance.GetComponent<Inventory>().inventoryItemNames[(int)value];
            promptText = "Take " + objectToDeliverName;
        }
    }
    public NeighbourAppearance currentScheduler;

    public bool hasDelivered = false;

    public override void Interact()
    {
        Player.Instance.GetComponent<Inventory>().inventory.Add(objectToDeliver);
        hasDelivered = true;
        currentScheduler.TriggerEventEnd();
    }

    public override bool CanInteract()
    {
        return !hasDelivered && _door.isOpen;
    }
}
