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
    private TimeSpan prevEndTime;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(OrderItem);
        notifcationMessage = "Order Confirmed! Your " + Player.Instance.newInventory.availableItemDict[objectBeingPurchased].displayName + " will arrive at ";
    }

    private void Update()
    {
        if (!isUiUpdated && deliveryNPC.hasDelivered)
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
        deliveryNPC.hasDelivered = false;

        TimeSpan currentTime = TimeController.Instance.CurrentTime();

        prevStartTime = deliveryNPC.Arrival;
        prevEndTime = deliveryNPC.Exit;

        bool hasPrevEventPassed = !TimeController.Instance.TimeHasPassed(prevEndTime.Hours, prevEndTime.Minutes);

        startTime = prevStartTime != TimeSpan.Zero && !TimeController.Instance.TimeHasPassed(prevEndTime.Hours, prevEndTime.Minutes) ? prevStartTime : currentTime.Add(new TimeSpan(1, 0, 0));
        endTime = prevEndTime != TimeSpan.Zero && !TimeController.Instance.TimeHasPassed(prevEndTime.Hours, prevEndTime.Minutes) ? prevEndTime : currentTime.Add(new TimeSpan(1, 30, 0));
        deliveryNPC.Arrival = startTime;
        deliveryNPC.Exit = endTime;

        if (!hasPrevEventPassed)
        {
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

        scrollWindow.enabled = false;

        arrivalTime = TimeController.Instance.TimeHasPassed(prevEndTime.Hours, prevEndTime.Minutes) ? TimeController.Instance.TimeSpanToClock(startTime) : TimeController.Instance.TimeSpanToClock(prevStartTime);
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
