using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piano : Interactable
{
    private AudioSource pianoAudio;
    private Animation noteAnim;
    public bool isPlaying;

    private const string PLAY = "Play";
    private const string STOP = "Stop";

    private void Start()
    {
        promptText = PLAY;
        pianoAudio = GetComponent<AudioSource>();
        noteAnim = GetComponent<Animation>();
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

            noteAnim.wrapMode = WrapMode.Loop;
            noteAnim.Play();

            // Lock player here
            Player.Instance.canMove = false;
            Player.Instance.GetComponent<PlayerInteract>().SetObjectAndChildrenHighlight(transform, false);

            // Toggle action
            promptText = STOP;
            isPlaying = true;
        } else
        {
            MusicController.Instance.playRandom = true;

            pianoAudio.Pause();
            noteAnim.wrapMode = WrapMode.Once;

            Player.Instance.canMove = true;
            Player.Instance.GetComponent<PlayerInteract>().SetObjectAndChildrenHighlight(transform, true);
            promptText = PLAY;
            isPlaying = false;
        }
    }

    private void ResetPianoTrack(object sender, System.EventArgs e)
    {
        pianoAudio.time = 0;
    }
}
