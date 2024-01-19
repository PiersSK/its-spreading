using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NeighbourAppearance : LimitedTimedEvent
{
    [SerializeField] private NavMeshAgent neighbour;
    [SerializeField] private Transform endPosition;

    private Vector3 startPosition;

    private void Start()
    {
        startPosition = neighbour.transform.position;
    }

    public override void TriggerEvent() {
        neighbour.SetDestination(endPosition.position);
    }

    public override void TriggerEventEnd()
    {
        neighbour.SetDestination(startPosition);
    }

}
