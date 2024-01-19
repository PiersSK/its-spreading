using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WineBottle : Interactable
{
    [SerializeField] private AudioClip glassClinkClip;
    private AudioSource _audioSource;
    private Animation _animation;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _animation = GetComponent<Animation>();
    }

    public override void Interact()
    {
        _audioSource.PlayOneShot(glassClinkClip);
        _animation.Play();
    }
}
