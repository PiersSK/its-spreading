using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryNPC : DialogueNPC
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
        Player.Instance.inventory.AddItem(objectToDeliver);
        hasDelivered = true;
    }
}
