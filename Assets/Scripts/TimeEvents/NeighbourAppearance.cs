using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NeighbourAppearance : LimitedTimedEvent
{
    public event EventHandler<EventArgs> NPCReachedDestination;
    private bool destinationReachedTriggered = false;
    private bool hasReturned = false;

    public NavMeshAgent neighbour;
    public Transform endPosition;
    private NPC neighbourNPC;

    private Vector3 startPosition;
    public bool neighbourIsOut;
    private float endRotationTimer = 0f;

    public bool npcIsEngaged = false;

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

            if (!destinationReachedTriggered)
            {
                NPCReachedDestination?.Invoke(this, new EventArgs());
                destinationReachedTriggered = true;
                SetWalkingAnimationIfPresent(false);
            }
        } 

        if (AtStartPoint() && !hasReturned)
        {
            hasReturned = true;
            SetWalkingAnimationIfPresent(false);
        }
    }

    public override bool ShouldEventEndTrigger()
    {
        return base.ShouldEventEndTrigger() && !npcIsEngaged;
    }

    public override void TriggerEvent() {
        SetWalkingAnimationIfPresent(true);
        neighbour.SetDestination(endPosition.position);
        neighbourIsOut = true;
    }

    public override void TriggerEventEnd()
    {
        SetWalkingAnimationIfPresent(true);
        neighbour.SetDestination(startPosition);
        neighbourIsOut = false;
        neighbourNPC.FadeNPCAudioOut(5f);
    }

    private bool AtEndPoint()
    {
        return neighbour.transform.position.x == endPosition.position.x
            && neighbour.transform.position.z == endPosition.position.z;
    }

    private bool AtStartPoint()
    {
        return neighbour.transform.position.x == startPosition.x
            && neighbour.transform.position.z == startPosition.z;
    }

    private void SetWalkingAnimationIfPresent(bool val)
    {
        if(neighbour.TryGetComponent<Animator>(out Animator animator))
        {
            animator.SetBool("isWalking", val);
        }
    }

}
