using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class NPC : MonoBehaviour
{
    [SerializeField] private AudioClip humClip;
    [SerializeField] private AudioClip whistleClip;
    [SerializeField] private AudioClip currentClip;

    private AudioSource npcAudio;
    private Animation noteAnim;
    private BackgroundMusicLayer npcAudioLayer;

    private float humAnimTimer = 0f;
    private float humAnimMinSpace = 1f;

    public bool audioIsPlaying = false;

    private void Start()
    {
        npcAudio = GetComponent<AudioSource>();
        noteAnim = GetComponent<Animation>();

        npcAudioLayer = new BackgroundMusicLayer(npcAudio);
    }

    private void Update()
    {
        if (currentClip == humClip && audioIsPlaying)
        {
            humAnimTimer += Time.deltaTime;
            if (humAnimTimer >= humAnimMinSpace)
            {
                if (Random.Range(0, 100) < 50) noteAnim.Play();
                humAnimTimer = 0f;
            }
        }
    }

    public void SetTrackToHum()
    {
        currentClip = humClip;
        noteAnim.wrapMode = WrapMode.Once;

    }

    public void SetTrackToWhistle()
    {
        currentClip = whistleClip;
        noteAnim.wrapMode = WrapMode.Loop;
    }

    public void PauseNPCAudio()
    {
        npcAudioLayer.PauseLayer();
        noteAnim.wrapMode = WrapMode.Once;
        audioIsPlaying = false;
    }

    public void PlayNPCAudio()
    {

        npcAudio.clip = currentClip;
        npcAudioLayer.PlayLayer();
        noteAnim.Play();
        audioIsPlaying = true;
    }

    public void FadeNPCAudioOut(float fadeDuration)
    {
        StartCoroutine(npcAudioLayer.FadeLayerOut(fadeDuration));
        noteAnim.wrapMode = WrapMode.Once;
        audioIsPlaying = false;
    }
}
