using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EddyBeddyPodcastEvent : LimitedTimedEvent
{
    [SerializeField] private Button callInButton;

    [Header("Eddy Pseudo NPC")]
    [SerializeField] private DialogueNPC eddyBeddy;

    private bool podcastActivated = false;

    private void Update()
    {
        if(!podcastActivated
            && TimeController.Instance.TimeHasPassed(eventHour, eventMinute)
            && !TimeController.Instance.TimeHasPassed(eventEndHour, eventEndMinute)
            && GossipSpreadingEvent.Instance.PlayerHasGossip()
        )
        {
            podcastActivated = true;
            callInButton.interactable = true;
            callInButton.onClick.AddListener(TalkToEddy);
        }
    }

    public override void TriggerEvent()
    {
        if (GossipSpreadingEvent.Instance.PlayerHasGossip())
        {
            podcastActivated = true;
            callInButton.interactable = true;
            callInButton.onClick.AddListener(TalkToEddy);
        }
    }

    public override void TriggerEventEnd()
    {
        callInButton.interactable = false;
        callInButton.onClick.RemoveListener(TalkToEddy);
    }

    private void TalkToEddy()
    {
        TimeController.Instance.ToggleTimePause();
        Player.Instance.TogglePlayerIsEngaged(true);
        DialogueUI.Instance.LoadJsonConversationToUI(eddyBeddy.dialogueFile, eddyBeddy);
        DialogueUI.Instance.gameObject.SetActive(true);
        PhoneUI.Instance.TogglePhone();

    }
}
