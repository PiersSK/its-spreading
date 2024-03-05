using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DeliveryNPC;

public class DeliveryNPC : DialogueNPC, IDataPersistence
{
    [SerializeField] private Door _door;
    public List<string> ObjectsToDeliver = new List<string>();

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

    public bool hasDelivered;

    public void LoadData(GameData data)
    {
        hasDelivered = data.hasReceivedDelivery;
    }
    
    public void SaveData(ref GameData data)
    {
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
        foreach(var item in ObjectsToDeliver)
        {
            Player.Instance.newInventory.AddItem(item);
        }
        hasDelivered = true;
        ObjectsToDeliver.Clear();
    }
}
