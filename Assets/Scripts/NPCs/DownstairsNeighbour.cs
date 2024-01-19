using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownstairsNeighbour : NPC
{
    [SerializeField] private AudioClip humClip;
    [SerializeField] private AudioClip whistleClip;
    [SerializeField] private AudioClip currentClip;

    private BackgroundMusicLayer npcAudioLayer;

    private float humAnimTimer = 0f;
    private float humAnimMinSpace = 1f;

    public bool audioIsPlaying = false;

    protected override void Start()
    {
        base.Start();
        npcAudioLayer = new BackgroundMusicLayer(npcAudio);
    }

    private void Update()
    {
        if (currentClip == humClip && audioIsPlaying)
        {
            humAnimTimer += Time.deltaTime;
            if (humAnimTimer >= humAnimMinSpace)
            {
                if (Random.Range(0, 100) < 50) npcAnim.Play();
                humAnimTimer = 0f;
            }
        }
    }

    public void SetTrackToHum()
    {
        currentClip = humClip;
        npcAnim.wrapMode = WrapMode.Once;

    }

    public void SetTrackToWhistle()
    {
        currentClip = whistleClip;
        npcAnim.wrapMode = WrapMode.Loop;
    }

    public override void PauseNPCAudio()
    {
        npcAudioLayer.PauseLayer();
        npcAnim.wrapMode = WrapMode.Once;
        audioIsPlaying = false;
    }

    public override void PlayNPCAudio()
    {

        npcAudio.clip = currentClip;
        npcAudioLayer.PlayLayer();
        npcAnim.Play();
        audioIsPlaying = true;
    }

    public override void FadeNPCAudioOut(float fadeDuration)
    {
        StartCoroutine(FadeLayerOut(fadeDuration));
        npcAnim.wrapMode = WrapMode.Once;
        audioIsPlaying = false;
    }
}
