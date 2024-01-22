using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryNPC : NPC
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
            objectToDeliverName = Player.Instance.GetComponent<Inventory>().inventoryItemNames[(int)value];
            promptText = "Take " + objectToDeliverName;
        }
    }

    public bool hasDelivered = false;

    public override void Interact()
    {
        Player.Instance.TogglePlayerIsEngaged();
        DialogueUI.Instance.LoadJsonConversationToUI(dialogueFile, this);
        DialogueUI.Instance.gameObject.SetActive(true);
    }

    public override bool CanInteract()
    {
        return !hasDelivered && _door.isOpen;
    }

    public override void NPCCoreAction()
    {
        Player.Instance.GetComponent<Inventory>().inventory.Add(objectToDeliver);
        hasDelivered = true;
    }
}
