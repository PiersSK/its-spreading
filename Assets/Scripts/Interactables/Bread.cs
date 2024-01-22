using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bread : Interactable
{
    public bool isSpreading = false;

    private const string JAM = "Jam";
    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        promptText = JAM;
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public override void Interact()
    {
        if (!isSpreading)
        {
            isSpreading = true;
            transform.Rotate(0, 180, 0, Space.Self);
            _audioSource.Play();
        }
    }

    public override bool CanInteract()
    {
        return !isSpreading && !TimeController.Instance.TimeHasPassed(10, 0);
    }
}