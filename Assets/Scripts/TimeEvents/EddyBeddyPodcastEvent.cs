using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EddyBeddyPodcastEvent : LimitedTimedEvent, IDataPersistence
{
    [SerializeField] private Button callInButton;

    [Header("Eddy Pseudo NPC")]
    [SerializeField] private DialogueNPC eddyBeddy;

    private bool podcastActivated = false;
    private const string SHOWSTARTNOTIF = "IT'S BEDTIME! Call into the show now!";
    private bool hasTalkedToEddy;

    public void LoadData(GameData data)
    {
        hasTalkedToEddy = data.hasTalkedToEddy;
    }

    public void SaveData(ref GameData data)
    {
        data.hasTalkedToEddy = hasTalkedToEddy;
    }
    private void Update()
    {
        if(!podcastActivated
            && TimeController.Instance.TimeHasPassed(eventHour, eventMinute)
            && !TimeController.Instance.TimeHasPassed(eventEndHour, eventEndMinute)
            && GossipSpreadingEvent.Instance.PlayerHasGossip()
            && !hasTalkedToEddy
        )
        {
            podcastActivated = true;
            callInButton.interactable = true;
            callInButton.onClick.AddListener(TalkToEddy);
        }
        if(hasTalkedToEddy)
        {
            podcastActivated = false;
            callInButton.interactable = false;
            callInButton.onClick.RemoveAllListeners();
        }
    }

    public override void TriggerEvent()
    {
        if (GossipSpreadingEvent.Instance.PlayerHasGossip())
        {
            Notification showStartNotif = PhoneUI.Instance.AddNotification(PhoneUI.PhoneApp.Bedheads, SHOWSTARTNOTIF);
            Button notifButton = showStartNotif.GetComponent<Button>();
            notifButton.onClick.RemoveAllListeners();
            notifButton.onClick.AddListener(OpenEddysBedheads);
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
        PhoneUI.Instance.TogglePhone();
        hasTalkedToEddy = true;
    }

    private void OpenEddysBedheads()
    {
        PhoneUI.Instance.eddybeddyPage.SetActive(true);
    }
}
