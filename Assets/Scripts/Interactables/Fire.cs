using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : Interactable
{
    [SerializeField] private ParticleSystem fireParticles;
    [SerializeField] private Light fireLight;
    private AudioSource _audioSource;

    public bool isLit = false;

    private void Start() // Always start unlit
    {
        _audioSource = GetComponent<AudioSource>();
        fireLight.intensity = 0f;
        var fireEmission = fireParticles.emission;
        fireEmission.rateOverTime = 0f;
    }

    private void Update()
    {
        if (isLit && !_audioSource.isPlaying) _audioSource.Play();
        else if (!isLit && _audioSource.isPlaying) _audioSource.Pause();
    }

    public void LightFire()
    {
        isLit = true;
        fireLight.intensity = 4f;
        var fireEmission = fireParticles.emission;
        fireEmission.rateOverTime = 20f;
    }

    public override void Interact()
    {
        isLit = false;
        fireLight.intensity = 0f;
        var fireEmission = fireParticles.emission;
        fireEmission.rateOverTime = 0f;
    }

    public override bool CanInteract()
    {
        return isLit;
    }
}
