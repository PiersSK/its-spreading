using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hob : Interactable
{
    [Header("Hob References")]
    [SerializeField] private Light hobLight;
    [SerializeField] private ParticleSystem hobSmoke;
    [SerializeField] private AudioClip ignitionSound;
    private AudioSource _audioSource;

    [Header("Fire Configuration")]
    [SerializeField] private Fire firstFire;
    [SerializeField] private Fire secondFire;
    [SerializeField] private float timeTillFire;
    [SerializeField] private float fireTimer = 0f; // Serialized for debug

    private bool fireStarted = false;
    private bool fireSpread = false;

    private int hobLevel = 0;
    private const int HOBMAX = 3;

    private const float HOBLIGHTMAX = 100f;
    private const float HOBSMOKEMAX = 20f;

    private const string TURNON = "Cook Bacon!";
    private const string INCREASEHEAT = "Increase Heat To ";
    private const string TURNOFF = "Turn Off Hob";

    private void Start()
    {
        promptText = TURNON;
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if(!fireStarted)
        {
            fireTimer += Time.deltaTime * hobLevel;
            if (fireTimer >= timeTillFire)
            {
                firstFire.LightFire();
                fireStarted = true;
                fireTimer = 0f;
                hobLevel = 0;
                UpdateHob();
            }
        }

        if (firstFire.isLit && !fireSpread)
        {
            fireTimer += Time.deltaTime * HOBMAX;
            if (fireTimer >= timeTillFire)
            {
                secondFire.LightFire();
                fireTimer = 0f;
                fireSpread = true;
            }
        }
    }

    private void UpdateHob()
    {
        if (hobLevel == 0)
            _audioSource.Pause();
        else
            _audioSource.Play();

        promptText = hobLevel == 0 ? TURNON : hobLevel < HOBMAX ? INCREASEHEAT + (hobLevel + 1) : TURNOFF;

        hobLight.intensity = HOBLIGHTMAX * ((float)hobLevel / HOBMAX);
        var smokeEmission = hobSmoke.emission;
        smokeEmission.rateOverTime = HOBSMOKEMAX * ((float)hobLevel / HOBMAX);
    }

    public override void Interact()
    {
        if (hobLevel == 0)
        {
            _audioSource.PlayOneShot(ignitionSound);
        }

        hobLevel = hobLevel < HOBMAX ? hobLevel + 1 : 0;
        UpdateHob();
    }

    public override bool CanInteract()
    {
        return !fireStarted;
    }
}
