using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorKnockEvent : TimedEvent
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip doorKnock;
    [SerializeField] private Animation doorKnockAnim;
    public override void TriggerEvent()
    {
        audioSource.PlayOneShot(doorKnock);
        doorKnockAnim.Play();
    }
}
