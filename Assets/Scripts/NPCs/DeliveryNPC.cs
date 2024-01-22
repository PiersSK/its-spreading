using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryNPC : DialogueNPC
{
    [SerializeField] private Door _door;

    private string objectToDeliverName;
    public Inventory.InventoryItem objectToDeliver;
    public Inventory.InventoryItem ObjectToDeliver
    {
        get { return objectToDeliver; }
        set
        { 
            objectToDeliver = value;
            objectToDeliverName = Player.Instance.GetComponent<Inventory>().GetItemName(value);
            promptText = "Take " + objectToDeliverName;
        }
    }

    public bool hasDelivered = false;

    public override void Interact()
    {
        base.Interact();
        GetComponent<Animator>().SetTrigger("handOver");
    }

    public override bool CanInteract()
    {
        return !hasDelivered && _door.isOpen;
    }

    public override void NPCCoreAction()
    {
        Player.Instance._inventory.AddToInventory(objectToDeliver);
        hasDelivered = true;
    }
}
