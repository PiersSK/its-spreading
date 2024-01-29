using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : Interactable, IDataPersistence
{
    [SerializeField] private ParticleSystem fireParticles;
    [SerializeField] private Light fireLight;

    [SerializeField] private ParticleSystem smokeParticles;
    [SerializeField] private float smokeLife;
    [SerializeField] private float smokeMaxEmission = 20f;
    [SerializeField] private float smokeMinEmission = 2f;
    private float smokeTimer = 0f;

    [SerializeField] private Material burntMaterial;
    [SerializeField] private List<Renderer> thingsToBurn;

    private AudioSource _audioSource;

    public bool isLit = false;
    public bool wasLit;

    public void LoadData(GameData data)
    {
        wasLit = data.fireStarted;
        if(wasLit)
        {
            LightFire();
            DouseFire();
        }

    }

    public void SaveData(ref GameData data)
    {

    }

    private const string PUTOUTTHOUGHT = "My soul feels lighter now.";

    private void Start() // Always start unlit
    {
        _audioSource = GetComponent<AudioSource>();
        fireLight.intensity = 0f;
        SetParticleEmission(fireParticles, 0f);
    }

    private void Update()
    {
        if (isLit && !_audioSource.isPlaying) _audioSource.Play();
        else if (!isLit && _audioSource.isPlaying) _audioSource.Pause();

        if(smokeTimer > 0)
        {
            smokeTimer -= Time.deltaTime;
            float smokeLevel = smokeMinEmission + (smokeMaxEmission - smokeMinEmission) * (smokeTimer / smokeLife);
            SetParticleEmission(smokeParticles, smokeLevel);
        }
    }

    private void SetParticleEmission(ParticleSystem particle, float emissionRate)
    {
        var emission = particle.emission;
        emission.rateOverTime = emissionRate;
    }

    public void LightFire()
    {
        isLit = true;
        wasLit = true;
        fireLight.intensity = 4f;
        SetParticleEmission(fireParticles, 20f);

        foreach(Renderer burnnItem in thingsToBurn)
        {
            burnnItem.material = burntMaterial;
        }
    }

    public override void Interact()
    {
        DouseFire();

        int firesLit = 0;
        foreach(Fire fire in FindObjectsOfType<Fire>())
            if(fire.isLit) firesLit++;
        if (firesLit == 0) // put out last fire
            ThoughtBubble.Instance.ShowThought(PUTOUTTHOUGHT);

    }

    private void DouseFire()
    {
        isLit = false;
        fireLight.intensity = 0f;
        SetParticleEmission(fireParticles, 0f);
        SetParticleEmission(smokeParticles, smokeMaxEmission);
        smokeTimer = smokeLife;
    } 

    public override bool CanInteract()
    {
        return isLit;
    }

}
