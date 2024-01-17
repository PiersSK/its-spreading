using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MusicController : MonoBehaviour
{
    public static MusicController Instance { get; private set; } // Singleton

    public event EventHandler<EventArgs> LoopReset;

    [SerializeField] private int numberOfSegments;

    private float segmentLengthSeconds;

    public bool playRandom = true;
    public int intensityLevel = 0;

    public float secondsThroughSegment = 0f;
    private AudioSource backgroundMusic;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        backgroundMusic = GetComponent<AudioSource>();

        segmentLengthSeconds = backgroundMusic.clip.length / numberOfSegments;
    }

    private void FixedUpdate()
    {
        if (backgroundMusic.isPlaying)
        {
            secondsThroughSegment += Time.deltaTime;
            if (secondsThroughSegment >= segmentLengthSeconds)
            {
                secondsThroughSegment = 0f;
                backgroundMusic.time = intensityLevel * segmentLengthSeconds;
                LoopReset?.Invoke(this, new EventArgs());

                if (playRandom)
                {
                    int increment = Random.Range(-1, 2);
                    if (intensityLevel == numberOfSegments - 1) increment = -1;
                    else if (intensityLevel == 0) increment = 1;

                    intensityLevel += increment;
                    intensityLevel = Mathf.Clamp(intensityLevel, 0, numberOfSegments - 1);
                }
            }
        }
    }

    public void SetVolume(float volume)
    {
        backgroundMusic.volume = volume;
    }

    public void PausePlayMusic()
    {
        if (backgroundMusic.isPlaying)
            backgroundMusic.Pause();
        else
            backgroundMusic.Play();
    }

    public void SetMusicIntensity(int intensity)
    {
        intensityLevel = intensity;
        if (intensity > numberOfSegments - 1)
        {
            intensityLevel = numberOfSegments - 1;
        } else if (intensity < 0)
        {
            intensityLevel = 0;
        }
    }
}
