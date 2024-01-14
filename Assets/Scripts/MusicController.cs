using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public static MusicController Instance { get; private set; } // Singleton

    [SerializeField] private int numberOfSegments;

    private float segmentLengthSeconds;

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
        secondsThroughSegment += Time.deltaTime;
        if(secondsThroughSegment >= segmentLengthSeconds)
        {
            secondsThroughSegment = 0f;
            backgroundMusic.time = intensityLevel * segmentLengthSeconds;

            // TEMPORARY: Randomly increases or lowers the music intensity
            intensityLevel += intensityLevel == numberOfSegments - 1 ? -1 : Random.Range(-1, 2);
            intensityLevel = Mathf.Clamp(intensityLevel, 0, numberOfSegments - 1);
        }
    }


}
