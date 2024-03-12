using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DeliveryNPC;

public class DeliveryNPC : DialogueNPC, IDataPersistence
{
    [SerializeField] private Door _door;
    public List<string> ObjectsToDeliver = new List<string>();
    public List<string> PendingOrders = new List<string>();

    public bool hasDelivered;
    public string deliveredTag = "-Delivered";

    public void addObjectToDeliver(string objectToDeliver)
    {
        ObjectsToDeliver.Add(objectToDeliver);
        if (ObjectsToDeliver.Count == 1 )
        {
            promptText = $"Take {Player.Instance.newInventory.availableItemDict[objectToDeliver].displayName}";
        }
        else
        {
            promptText = "Take items";
        }
    }

    public bool hasOrderedOrDeliveredItem(string item)
    {
        return ObjectsToDeliver.Contains(item + deliveredTag) || ObjectsToDeliver.Contains(item);
    }

    public void LoadData(GameData data)
    {
        //ObjectsToDeliver = data.itemsBeingDelivered;
        hasDelivered = data.hasReceivedDelivery;
    }
    
    public void SaveData(ref GameData data)
    {
        //data.itemsBeingDelivered = ObjectsToDeliver;
        data.hasReceivedDelivery = hasDelivered;
    }

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
        for(int i = 0; i < ObjectsToDeliver.Count; i++)
        {
            if (!ObjectsToDeliver[i].EndsWith(deliveredTag))
            {
                Player.Instance.newInventory.AddItem(ObjectsToDeliver[i]);
                ObjectsToDeliver[i] = ObjectsToDeliver[i] + deliveredTag;
            }
        }
        hasDelivered = true;
    }
}
