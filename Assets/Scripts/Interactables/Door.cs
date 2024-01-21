using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    [SerializeField] private AudioClip doorKnock;
    [SerializeField] private Animation doorKnockAnim;

    private Animator doorAnimation;
    private AudioSource _audioSource;
    public bool isOpen = false;

    private const string OPENPROMPT = "Open Door";
    private const string CLOSEPROMPT = "Close Door";

    private void Start()
    {
        doorAnimation = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        promptText = OPENPROMPT;
    }

    public override void Interact()
    {
        isOpen = !isOpen;
        doorAnimation.SetBool("isOpen", isOpen);
        promptText = isOpen ? CLOSEPROMPT : OPENPROMPT;

        if(isOpen)
        {
            _audioSource.Pause();
            doorKnockAnim.Stop();
        }
    }

    public void KnockAtDoor()
    {
        if (!isOpen)
        {
            _audioSource.PlayOneShot(doorKnock);
            doorKnockAnim.Play();
        }
    }
}
