using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Overlays;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class SisterInteractionEvent : LimitedTimedEvent
{
    [Header("Message Blocks")]
    [SerializeField] private GameObject sisMessages;
    [SerializeField] private GameObject firstMessageBlock;
    [SerializeField] private GameObject responsePrompt;
    [SerializeField] private GameObject ignoreMessage;
    [SerializeField] private Image coversationBlock;
    [SerializeField] private TextMeshProUGUI messagePreview;

    [Header("Call Info")]
    [SerializeField] private Button callSisButton;
    [SerializeField] private DialogueNPC sisNPC;
    [SerializeField] private Button bookTicketsButton;

    private const string FIRSTNOTIFMESSAGE = "Hey big bro, how are you doing? :)";
    private const string SECONDNOTIFMESSAGE = "Ahh nevermind, I know you're busy. Speak soon x";

    private void Start()
    {
        callSisButton.onClick.AddListener(CallSis);
    }

    public override void TriggerEvent()
    {
        firstMessageBlock.SetActive(true);
        messagePreview.text = FIRSTNOTIFMESSAGE;
        coversationBlock.color = new Color(0.32f, 0.55f, 0.32f);
        AddNotificationFromSis(FIRSTNOTIFMESSAGE);

        BookTicketsEvent ticketEvent = Instantiate(Resources.Load<BookTicketsEvent>("DynamicEvents/TicketEvent"), TimeController.Instance.scheduledEvents);

        ticketEvent.bookButton = bookTicketsButton;

        TimeSpan currentTime = TimeController.Instance.CurrentTime();
        TimeSpan startTime = currentTime.Add(new TimeSpan(Random.Range(1,3), Random.Range(0,60), 0));
        TimeSpan endTime = startTime.Add(new TimeSpan(0, 45, 0));

        ticketEvent.SetEventStartTime(startTime.Hours, startTime.Minutes);
        ticketEvent.SetEventEndTime(endTime.Hours, endTime.Minutes);
    }

    public override void TriggerEventEnd()
    {
        if (!LoveSpreadingEvent.Instance.calledSis)
        {
            responsePrompt.SetActive(false);
            ignoreMessage.SetActive(true);
            messagePreview.text = SECONDNOTIFMESSAGE;
            AddNotificationFromSis(SECONDNOTIFMESSAGE);
        }

    }

    private void OpenSisConversation()
    {
        PhoneUI.Instance.messagePage.SetActive(true);
        sisMessages.SetActive(true);
    }

    private void AddNotificationFromSis(string message)
    {
        Notification sisNotif = PhoneUI.Instance.AddNotification(PhoneUI.PhoneApp.Messages, message);
        Button notifButton = sisNotif.GetComponent<Button>();
        notifButton.onClick.RemoveAllListeners();
        notifButton.onClick.AddListener(OpenSisConversation);
    }

    private void CallSis()
    {
        Player.Instance.TogglePlayerIsEngaged(true);
        DialogueUI.Instance.LoadJsonConversationToUI(sisNPC.dialogueFile, sisNPC);
        DialogueUI.Instance.gameObject.SetActive(true);
        PhoneUI.Instance.TogglePhone();

        responsePrompt.SetActive(false);
    }
}
