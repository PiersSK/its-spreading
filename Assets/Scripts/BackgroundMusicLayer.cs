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

    public IEnumerator FadeLayerOut(float fadeDuration)
    {
        float currentTime = 0;
        float start = audioSource.volume;
        while (currentTime < fadeDuration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, 0, currentTime / fadeDuration);
            yield return null;
        }
        yield break;
    }
}
