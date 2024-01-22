using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    [SerializeField] private AudioClip doorKnock;
    [SerializeField] private AudioClip doorOpenSound;
    [SerializeField] private Animation doorKnockAnim;
    [SerializeField] private AudioSource knockSource;
    [SerializeField] private AudioSource doorSource;

    [Range(1,10)]
    [SerializeField] private float distanceTillClose;

    private Animator doorAnimation;
    public bool isOpen = false;

    private const string OPENPROMPT = "Open Door";
    private const string CLOSEPROMPT = "Close Door";

    private void Update()
    {
        if (Vector3.Distance(transform.position, Player.Instance.transform.position) > distanceTillClose && isOpen)
        {
            ToggleDoorOpenState();
        }
    }

    private void Start()
    {
        doorAnimation = GetComponent<Animator>();
        promptText = OPENPROMPT;
    }

    public override void Interact()
    {
        ToggleDoorOpenState();
    }

    private void ToggleDoorOpenState()
    {
        isOpen = !isOpen;
        doorAnimation.SetBool("isOpen", isOpen);
        promptText = isOpen ? CLOSEPROMPT : OPENPROMPT;
        doorSource.PlayOneShot(doorOpenSound);

        if (isOpen)
        {
            knockSource.Pause();
            doorKnockAnim.Stop();
        }
    }

    public void KnockAtDoor()
    {
        if (!isOpen)
        {
            knockSource.PlayOneShot(doorKnock);
            doorKnockAnim.Play();
        }
    }
}
