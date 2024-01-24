using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BookTicketsEvent : LimitedTimedEvent
{
    public Button bookButton;

    public override void TriggerEvent()
    {
        bookButton.GetComponentInChildren<TextMeshProUGUI>().text = "Book Now";
        bookButton.interactable = true;
        PhoneUI.Instance.AddNotification(PhoneUI.PhoneApp.NeverTooLate, "2 Tickets Listed For \"As Easy As 1, You, Me\"");
    }

    public override void TriggerEventEnd()
    {
        bookButton.GetComponentInChildren<TextMeshProUGUI>().text = "0 Tickets Available";
        bookButton.interactable = true;
        PhoneUI.Instance.AddNotification(PhoneUI.PhoneApp.NeverTooLate, "\"As Easy As 1, You, Me\" is now sold out");

    }
}
