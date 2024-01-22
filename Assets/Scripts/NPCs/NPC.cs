using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class NPC : Interactable
{
    protected AudioSource npcAudio;
    protected Animation npcAnim;
    public NeighbourAppearance currentScheduler;
    [SerializeField] protected TextAsset dialogueFile;


    public Color primaryColor;
    public Color secondaryColor;

    protected virtual void Start()
    {
        npcAudio = GetComponent<AudioSource>();
        npcAnim = GetComponent<Animation>();
    }

    public virtual void NPCCoreAction() { }

    public virtual void PauseNPCAudio()
    {
        npcAudio.Pause();
    }

    public virtual void PlayNPCAudio()
    {

        npcAudio.Play();
    }

    public virtual void FadeNPCAudioOut(float fadeDuration)
    {
        StartCoroutine(FadeLayerOut(fadeDuration));
    }

    protected IEnumerator FadeLayerOut(float fadeDuration)
    {
        float currentTime = 0;
        float start = npcAudio.volume;
        while (currentTime < fadeDuration)
        {
            currentTime += Time.deltaTime;
            npcAudio.volume = Mathf.Lerp(start, 0, currentTime / fadeDuration);
            yield return null;
        }
        yield break;
    }

    public override bool CanInteract()
    {
        return false;
    }
}
