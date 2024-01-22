using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bread : Interactable
{
    public bool isSpreading = false;

    private const string BUTTER = "Butter";
    private AudioSource _audioSource;
    [SerializeField] AudioClip _audioClip;

    // Start is called before the first frame update
    void Start()
    {
        promptText = BUTTER;
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public override void Interact()
    {
        if(!isSpreading)
        {
            isSpreading = true;
            _audioSource.PlayOneShot(_audioClip);
            this.transform.Rotate(0,180,0, Space.Self);
        }
    }

    public override bool CanInteract()
    {
        return !isSpreading;
    }
}