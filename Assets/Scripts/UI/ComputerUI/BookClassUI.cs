using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BookClassUI : MonoBehaviour
{
    [SerializeField] private Transform chair;
    [SerializeField] private Transform chairAwayPos;
    [SerializeField] private Transform dancePos;

    [SerializeField] private List<GameObject> discoLights;

    [SerializeField] private AudioSource computerAudio;
    [SerializeField] private AudioSource boomboxAudio;
    [SerializeField] private AudioSource bgMusicAudio;

    [SerializeField] private AudioClip bookingConfirmed;
    [SerializeField] private AudioClip remixSong;

    [SerializeField] private DanceClassEvent danceClassEvent;


    private const string BOOKINGTHOUGHT = "I've always wanted to try this since it became legal again, but it's so scary...";

    private void Start()
    {
        if(danceClassEvent.playerSignedUp && !danceClassEvent.classHasStarted)
        {
            GetComponentInChildren<TextMeshProUGUI>().text = "Booked! See you at 8pm";
            GetComponent<Button>().interactable = false;
        }
       GetComponent<Button>().onClick.AddListener(DanceClass);
    }

    private void BookClass()
    {
        GetComponentInChildren<TextMeshProUGUI>().text = "Booked! See you at 8pm";
        danceClassEvent.playerSignedUp = true; 

        computerAudio.PlayOneShot(bookingConfirmed);

        ThoughtBubble.Instance.ShowThought(PlayerThoughts.BookDanceClass);

        GetComponent<Button>().interactable = false;
        GetComponent<Button>().onClick.RemoveAllListeners();
        GetComponent<Button>().onClick.AddListener(JoinClass);
    }

    private void JoinClass()
    {
        Player.Instance.transform.position = dancePos.position;
        chair.position = chairAwayPos.position;

        bgMusicAudio.Pause();
        boomboxAudio.clip = remixSong;
        boomboxAudio.Play();

        TimeController.Instance.TurnOffAllLights();
        foreach(GameObject obj in discoLights) obj.SetActive(true);
        CameraController.Instance.SetCameraZoom(5f, 10f);
        boomboxAudio.GetComponent<Animator>().SetBool("isPlaying", true);

        Player.Instance._animator.SetBool("isDancing", true);
        ComputerUI.Instance.gameObject.SetActive(false);
    }

    private void DanceClass()
    {
        if(danceClassEvent.playerSignedUp && danceClassEvent.classHasStarted)
        {
            JoinClass();
        }
        else
        {
            BookClass();
        }
    }
}
