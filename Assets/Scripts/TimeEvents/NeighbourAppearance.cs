using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NeighbourAppearance : LimitedTimedEvent
{
    [SerializeField] private NavMeshAgent neighbour;
    [SerializeField] private Transform endPosition;
    private NPC neighbourNPC;

    private Vector3 startPosition;
    public bool neighbourIsOut;
    private float endRotationTimer = 0f;

    private void Start()
    {
        startPosition = neighbour.transform.position;
        neighbourNPC = neighbour.GetComponent<NPC>();
    }

    private void Update()
    {
        if (AtEndPoint())
        {
            neighbour.transform.rotation = Quaternion.Slerp(neighbour.transform.rotation, Quaternion.LookRotation(endPosition.forward), 1f);
            if(endRotationTimer < 1f) endRotationTimer += Time.deltaTime / 3f;
        } 
    }

    public override void TriggerEvent() {
        neighbour.SetDestination(endPosition.position);
        neighbourIsOut = true;
    }

    public override void TriggerEventEnd()
    {
        neighbour.SetDestination(startPosition);
        neighbourIsOut = false;
        neighbourNPC.FadeNPCAudioOut(5f);
    }

    private bool AtEndPoint()
    {
        return neighbour.transform.position.x == endPosition.position.x
            && neighbour.transform.position.z == endPosition.position.z;
    }

}
