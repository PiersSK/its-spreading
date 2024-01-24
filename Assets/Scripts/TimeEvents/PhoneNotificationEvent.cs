using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneNotificationEvent : TimedEvent
{
    [SerializeField] private PhoneUI.PhoneApp appSource;
    [SerializeField] private string message;

    public override void TriggerEvent()
    {
        PhoneUI.Instance.AddNotification(appSource, message);
    }
}
