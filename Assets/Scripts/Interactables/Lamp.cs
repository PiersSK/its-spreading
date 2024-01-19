using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : Interactable
{
    [SerializeField] private GameObject lampLight;
    [SerializeField] AudioClip switchSound;
    private AudioSource _audioSource;

    private const string ONPROMPT = "Switch On";
    private const string OFFPROMPT = "Switch Off";

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        promptText = lampLight.activeSelf ? OFFPROMPT : ONPROMPT;

    }

    public override void Interact()
    {
        lampLight.SetActive(!lampLight.activeSelf);
        _audioSource.PlayOneShot(switchSound);
        promptText = lampLight.activeSelf ? OFFPROMPT : ONPROMPT;
    }
}
