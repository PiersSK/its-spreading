using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public static MusicController Instance { get; private set; } // Singleton

    [SerializeField] private int numberOfSegments;
    [SerializeField] private int finalStandardSegment;

    public enum SpecialMusicSegment
    {
        MaxIntensity,
        Piano
    }

    private float segmentLengthSeconds;

    public bool playRandom = true;
    public int intensityLevel = 0;

    private float secondsThroughSegment = 0f;
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

                if (playRandom)
                {
                    int increment = Random.Range(-1, 2);
                    if (intensityLevel == finalStandardSegment) increment = -1;
                    else if (intensityLevel == 0) increment = 1;

                    intensityLevel += increment;
                    intensityLevel = Mathf.Clamp(intensityLevel, 0, finalStandardSegment);
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

    public void SetMusicIntensity(SpecialMusicSegment segment)
    {
        int intensity = finalStandardSegment + (int)segment;

        intensityLevel = intensity;
        if (intensity > numberOfSegments - 1)
        {
            Debug.LogWarning("Tried to set intensity to " + intensity + " but maximum is " + (numberOfSegments - 1) + ". Setting to max instead");
            intensityLevel = numberOfSegments - 1;
        }
        else if (intensity < 0)
        {
            Debug.LogWarning("Tried to set intensity to " + intensity + " but minimum is 0. Setting to 0 instead");
            intensityLevel = 0;
        }
    }
}
