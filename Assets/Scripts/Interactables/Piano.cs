using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Piano : LockinInteractable
{
    private AudioSource pianoAudio;
    private Animation noteAnim;
    private BackgroundMusicLayer pianoLayer;

    private bool hasReacted = false;

    private void Start()
    {
        pianoAudio = GetComponent<AudioSource>();
        noteAnim = GetComponent<Animation>();
        MusicController.Instance.LoopReset += ResetPianoTrack;

        pianoLayer = new BackgroundMusicLayer(pianoAudio);
    }

    protected override void LockedInInteract()
    {
        base.LockedInInteract();

        MusicController.Instance.playRandom = true;
        pianoLayer.PauseLayer();

        noteAnim.wrapMode = WrapMode.Once;

        Player.Instance._animator.SetBool("isPlayingPiano", false);

    }

    protected override void FreeInteract()
    {
        base.FreeInteract();

        if(!hasReacted)
        {
            ThoughtBubble.Instance.ShowThought(PlayerThoughts.PlayingPiano);
            hasReacted = true;
        }

        // Set background music correctly
        MusicController.Instance.SetMusicIntensity(1);
        MusicController.Instance.playRandom = false;

        // Play piano intime with bg music
        pianoLayer.PlayLayer();

        noteAnim.wrapMode = WrapMode.Loop;
        noteAnim.Play();

        Player.Instance._animator.SetBool("isPlayingPiano", true);

    }

    private void ResetPianoTrack(object sender, System.EventArgs e)
    {
        pianoAudio.time = 0;
    }
}
