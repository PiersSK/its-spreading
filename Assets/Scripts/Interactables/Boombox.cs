using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boombox : Interactable
{
    [SerializeField] private AudioSource bgMusic;
    [SerializeField] private List<GameObject> discoLights;
    private AudioSource _audioSource;
    private Animator _animator;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
    }

    public override bool CanInteract()
    {
        return _audioSource.isPlaying;
    }

    public override void Interact()
    {
        _audioSource.Pause();
        bgMusic.Play();

        _animator.SetBool("isPlaying", false);

        TimeController.Instance.TurnOnAllLights();
        foreach(GameObject obj in discoLights) { obj.SetActive(false); }
    }
}
