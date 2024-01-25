using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryNPC : DialogueNPC
{
    [SerializeField] private Door _door;

    private string objectToDeliverName;

    private string objectToDeliver;
    public string ObjectToDeliver
    {
        get { return objectToDeliver; }
        set
        { 
            objectToDeliver = value;
            objectToDeliverName = Player.Instance.newInventory.availableItemDict[objectToDeliver].displayName;
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
        Debug.Log($"Given {objectToDeliverName}");
        Player.Instance.newInventory.AddItem(objectToDeliver);
        hasDelivered = true;
    }
}
