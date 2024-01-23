using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Notification : MonoBehaviour
{
    public PhoneUI.PhoneApp appsource;
    public string messageBody;
    public Color backgroundColor;

    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI body;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(NotificationClicked);
        UpdateNotification();
    }

    public void UpdateNotification()
    {
        backgroundColor.a = 0.75f;
        GetComponent<Image>().color = backgroundColor;
        title.text = appsource.ToString();
        body.text = messageBody;
    }

    private void NotificationClicked()
    {
        switch (appsource)
        {
            case PhoneUI.PhoneApp.Messages:
                //PhoneUI.Instance.notificationPage.SetActive(false);
                PhoneUI.Instance.messagePage.SetActive(true);
                break;
            default:
                break;
        }

        PhoneUI.Instance.notifications.Remove(this);
        Destroy(gameObject);
    }
}