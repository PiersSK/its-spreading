using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NeighbourAppearance : LimitedTimedEvent, IDataPersistence
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

    public void LoadData(GameData data)
    {
        destinationReachedTriggered = data.hasNeighbourEventTriggered;
    }

    public void SaveData(ref GameData data)
    {
        data.hasNeighbourEventTriggered = destinationReachedTriggered;
    }

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

            if (!destinationReachedTriggered) OnComplete();
        }

        if (AtStartPoint() && destinationReachedTriggered && !hasReturned)
        {
            Debug.Log("jjjsdkfjhsdjk");
            OnReturn();
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

    protected virtual void OnComplete()
    {
        NPCReachedDestination?.Invoke(this, new EventArgs());
        destinationReachedTriggered = true;
        SetWalkingAnimationIfPresent(false);
    }
    protected virtual void OnReturn()
    {
        hasReturned = true;
        SetWalkingAnimationIfPresent(false);

    }

    private bool AtEndPoint()
    {
        Vector2 neighbourXZ = new Vector2(neighbour.transform.position.x, neighbour.transform.position.z);
        Vector2 endXZ = new Vector2(endPosition.position.x, endPosition.position.z);
        return Mathf.Round(Vector2.Distance(neighbourXZ, endXZ) * 10) == 0;
    }

    private bool AtStartPoint()
    {
        Vector2 neighbourXZ = new Vector2(neighbour.transform.position.x, neighbour.transform.position.z);
        Vector2 endXZ = new Vector2(startPosition.x, startPosition.z);
        return Mathf.Round(Vector2.Distance(neighbourXZ, endXZ) * 10) == 0;
    }
    private void SetWalkingAnimationIfPresent(bool val)
    {
        if(neighbour.TryGetComponent<Animator>(out Animator animator))
        {
            animator.SetBool("isWalking", val);
        }
    }

}
