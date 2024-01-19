using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sink : Interactable
{
    private AudioSource _audioSource;
    [SerializeField] private GameObject water;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public override void Interact()
    {
        water.SetActive(!water.activeSelf);
        if (_audioSource.isPlaying)
            _audioSource.Pause();
        else
            _audioSource.Play();
    }
}
