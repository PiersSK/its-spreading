using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BookTicketsUI : MonoBehaviour
{
    [SerializeField] private GameObject callSisBlock;
    [SerializeField] private Button callSisButton;
    [SerializeField] private DialogueNPC sisNPC;


    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(BookTickets);
    }

    private void BookTickets()
    {
        LoveSpreadingEvent.Instance.bookedTickets = true;
        GetComponent<Button>().interactable = false;
        GetComponentInChildren<TextMeshProUGUI>().text = "Booked!";

        ThoughtBubble.Instance.ShowThought("Sis will love these! I should call her to tell her the good news");
        callSisBlock.SetActive(true);

        callSisButton.onClick.RemoveAllListeners();
        callSisButton.onClick.AddListener(CallSis);

        Invoke(nameof(SendBookingConfirmation), 5f);
    }

    private void SendBookingConfirmation()
    {
        PhoneUI.Instance.AddNotification(PhoneUI.PhoneApp.NeverTooLate, "Booking confirmed for \"As Easy As 1, You, Me\" x2");
    }

    private void CallSis()
    {
        TimeController.Instance.ToggleTimePause();
        Player.Instance.TogglePlayerIsEngaged(true);
        DialogueUI.Instance.LoadJsonConversationToUI(sisNPC.dialogueFile, sisNPC, 1);
        DialogueUI.Instance.gameObject.SetActive(true);
        PhoneUI.Instance.TogglePhone();

        callSisBlock.SetActive(false);
    }
}
