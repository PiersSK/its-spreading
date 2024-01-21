using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class OrderItemUI : MonoBehaviour
{
    [SerializeField] private NavMeshAgent deliveryNPC;
    [SerializeField] private Transform doorPosition;
    [SerializeField] private Door _door;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(OrderItem);
    }

    private void OrderItem()
    {
        NeighbourAppearance npcArrival = Instantiate(Resources.Load<NeighbourAppearance>("DynamicEvents/NPCArrival"), TimeController.Instance.scheduledEvents);

        npcArrival.neighbour = deliveryNPC;
        npcArrival.endPosition = doorPosition;
        TimeSpan currentTime = TimeController.Instance.CurrentTime();
        TimeSpan startTime = currentTime.Add(new TimeSpan(1, 0, 0));
        TimeSpan endTime = currentTime.Add(new TimeSpan(1, 30, 0));

        npcArrival.SetEventStartTime(startTime.Hours, startTime.Minutes);
        npcArrival.SetEventEndTime(endTime.Hours, endTime.Minutes);

        npcArrival.NPCReachedDestination += NpcArrival_NPCReachedDestination;
        // schedule door knock
        // share info about what's being delivered
    }

    private void NpcArrival_NPCReachedDestination(object sender, EventArgs e)
    {
        _door.KnockAtDoor();
    }
}
