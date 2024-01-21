using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class OrderItemUI : MonoBehaviour
{

    [SerializeField] private Inventory.InventoryItem objectBeingPurchased;

    [Header("References")]
    [SerializeField] private DeliveryNPC deliveryNPC;
    [SerializeField] private Transform doorPosition;
    [SerializeField] private Door _door;

    [SerializeField] private ScrollRect scrollWindow;
    [SerializeField] private GameObject purchaseConfirmed;
    [SerializeField] private TextMeshProUGUI purchaseConfirmedTime;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(OrderItem);
    }

    private void OrderItem()
    {

        NeighbourAppearance npcArrival = Instantiate(Resources.Load<NeighbourAppearance>("DynamicEvents/NPCArrival"), TimeController.Instance.scheduledEvents);

        DeliveryNPC _deliverNPC = deliveryNPC.GetComponent<DeliveryNPC>();

        deliveryNPC.ObjectToDeliver = objectBeingPurchased;
        deliveryNPC.currentScheduler = npcArrival;
        deliveryNPC.hasDelivered = false;

        npcArrival.neighbour = deliveryNPC.GetComponent<NavMeshAgent>();
        npcArrival.endPosition = doorPosition;
        TimeSpan currentTime = TimeController.Instance.CurrentTime();
        TimeSpan startTime = currentTime.Add(new TimeSpan(1, 0, 0));
        TimeSpan endTime = currentTime.Add(new TimeSpan(1, 30, 0));


        npcArrival.SetEventStartTime(startTime.Hours, startTime.Minutes);
        npcArrival.SetEventEndTime(endTime.Hours, endTime.Minutes);

        npcArrival.NPCReachedDestination += NpcArrival_NPCReachedDestination;

        GetComponent<Button>().interactable = false;
        GetComponent<Image>().color = new Color(0.55f, 0.55f, 0.55f);
        GetComponent<Button>().transform.GetComponentInChildren<TextMeshProUGUI>().text = "Out Of Stock";

        scrollWindow.enabled = false;
        purchaseConfirmedTime.text = TimeController.Instance.TimeSpanToClock(startTime);
        purchaseConfirmed.SetActive(true);
    }

    private void NpcArrival_NPCReachedDestination(object sender, EventArgs e)
    {
        _door.KnockAtDoor();
    }
}
