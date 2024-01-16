using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piano : Interactable
{
    private AudioSource pianoAudio;
    private bool isPlaying;

    private const string PLAY = "Play";
    private const string STOP = "Stop";

    private void Start()
    {
        promptText = PLAY;
        pianoAudio = GetComponent<AudioSource>();
        MusicController.Instance.LoopReset += ResetPianoTrack;

    }

    public override void Interact()
    {
        if (!isPlaying)
        {
            // Set background music correctly
            MusicController.Instance.SetMusicIntensity(1);
            MusicController.Instance.playRandom = false;

            // Play piano intime with bg music
            pianoAudio.time = MusicController.Instance.secondsThroughSegment;
            pianoAudio.Play();

            // Lock player here
            Player.Instance.canMove = false;

            // Toggle action
            promptText = STOP;
            isPlaying = true;
        } else
        {
            MusicController.Instance.playRandom = true;
            pianoAudio.Pause();
            Player.Instance.canMove = true;
            promptText = PLAY;
            isPlaying = false;
        }
    }

    private void ResetPianoTrack(object sender, System.EventArgs e)
    {
        pianoAudio.time = 0;
    }

    public override bool ShouldHighlight()
    {
        return !isPlaying;
    }
}
