using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicLayer
{
    private AudioSource audioSource;

    public BackgroundMusicLayer(AudioSource source)
    {
        audioSource = source;
        MusicController.Instance.LoopReset += ResetTrack;
    }

    private void ResetTrack(object sender, System.EventArgs e)
    {
        audioSource.time = 0f;
    }

    public void PlayLayer()
    {
        // Play intime with bg music
        audioSource.time = MusicController.Instance.secondsThroughSegment;
        audioSource.Play();
    }

    public void PauseLayer()
    {
        audioSource.Pause();
    }
}
