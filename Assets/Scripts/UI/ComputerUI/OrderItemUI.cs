using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class OrderItemUI : MonoBehaviour
{

    [SerializeField] private string objectBeingPurchased;
    [SerializeField] private AudioClip purchaseConfirmSound;


    [Header("References")]
    [SerializeField] private DeliveryNPC deliveryNPC;
    [SerializeField] private Transform doorPosition;
    [SerializeField] private Door _door;

    [SerializeField] private ScrollRect scrollWindow;
    [SerializeField] private GameObject purchaseConfirmed;
    [SerializeField] private TextMeshProUGUI purchaseConfirmedTime;
    private bool isUiUpdated = false;

    private string notifcationMessage;
    private string arrivalTime;
    private TimeSpan startTime = TimeSpan.Zero;
    private TimeSpan endTime = TimeSpan.Zero;
    private TimeSpan prevStartTime;
    private bool prevEventStarted = false;
    private bool prevEventEnded = true;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(OrderItem);
        notifcationMessage = "Order Confirmed! Your " + Player.Instance.newInventory.availableItemDict[objectBeingPurchased].displayName + " will arrive at ";
    }

    private void Update()
    {
        if (!isUiUpdated && deliveryNPC.hasOrderedOrDeliveredItem(objectBeingPurchased))
        {
            GetComponent<Button>().interactable = false;
            GetComponent<Image>().color = new Color(0.55f, 0.55f, 0.55f);
            GetComponent<Button>().transform.GetComponentInChildren<TextMeshProUGUI>().text = "Out Of Stock";
            isUiUpdated = true;
        }
    }

    private void OrderItem()
    {

        DeliveryNPC _deliverNPC = deliveryNPC.GetComponent<DeliveryNPC>();

        deliveryNPC.addObjectToDeliver(objectBeingPurchased);

        if(deliveryNPC.currentScheduler != null) //we have a delivery event already scheduled
        {
            prevStartTime = deliveryNPC.currentScheduler.GetScheduledStartTime();
            prevEventStarted = deliveryNPC.currentScheduler.hasBeenTriggered;
            prevEventEnded = deliveryNPC.currentScheduler.eventHasEnded;
        }

        TimeSpan currentTime = TimeController.Instance.CurrentTime();
        startTime = prevEventStarted ? prevStartTime : currentTime.Add(new TimeSpan(1, 0, 0));
        endTime = currentTime.Add(new TimeSpan(1, 30, 0));

        if ( prevEventEnded || deliveryNPC.hasDelivered)
        {
            startTime = currentTime.Add(new TimeSpan(1,0,0));
            NeighbourAppearance npcArrival = Instantiate(Resources.Load<NeighbourAppearance>("DynamicEvents/NPCArrival"), TimeController.Instance.scheduledEvents);
            deliveryNPC.currentScheduler = npcArrival;
            npcArrival.neighbour = deliveryNPC.GetComponent<NavMeshAgent>();
            npcArrival.endPosition = doorPosition;

            npcArrival.SetEventStartTime(startTime.Hours, startTime.Minutes);
            npcArrival.SetEventEndTime(endTime.Hours, endTime.Minutes);

            npcArrival.NPCReachedDestination += NpcArrival_NPCReachedDestination;
        }
        GetComponent<Button>().interactable = false;
        GetComponent<Image>().color = new Color(0.55f, 0.55f, 0.55f);
        GetComponent<Button>().transform.GetComponentInChildren<TextMeshProUGUI>().text = "Out Of Stock";

        deliveryNPC.hasDelivered = false;
        scrollWindow.enabled = false;

        arrivalTime = TimeController.Instance.TimeSpanToClock(startTime);
        purchaseConfirmedTime.text = arrivalTime;
        purchaseConfirmed.SetActive(true);

        Invoke(nameof(SendNotification), 5f);

        ComputerUI.Instance.computerAudio.PlayOneShot(purchaseConfirmSound);
    }

    private void SendNotification()
    {
        PhoneUI.Instance.AddNotification(PhoneUI.PhoneApp.Spreadshop, notifcationMessage + arrivalTime);
    }

    private void NpcArrival_NPCReachedDestination(object sender, EventArgs e)
    {
        _door.KnockAtDoor();
    }
}
