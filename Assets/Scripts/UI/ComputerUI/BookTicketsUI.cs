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

    private bool bookedTickets = false;
    private bool madeSisAvailable = false;
    private int sisAvailableHour = 18;


    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(BookTickets);
    }

    private void BookTickets()
    {
        bookedTickets = true;
        LoveSpreadingEvent.Instance.bookedTickets = true;
        GetComponent<Button>().interactable = false;
        GetComponentInChildren<TextMeshProUGUI>().text = "Booked!";

        if(LoveSpreadingEvent.Instance.calledSis && LoveSpreadingEvent.Instance.wasKindToSis)
            ThoughtBubble.Instance.ShowThought(PlayerThoughts.BookShowTickets);

        Invoke(nameof(SendBookingConfirmation), 5f);
    }

    private void SendBookingConfirmation()
    {
        PhoneUI.Instance.AddNotification(PhoneUI.PhoneApp.NeverTooLate, "Booking confirmed for \"As Easy As 1, You, Me\" x2");
    }
}
