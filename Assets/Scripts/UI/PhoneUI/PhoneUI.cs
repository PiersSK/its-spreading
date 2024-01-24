using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class PhoneUI : MonoBehaviour
{ 
    public static PhoneUI Instance { get; private set; }
    
    public enum PhoneApp
    {
        Messages,
        Spreadshop,
        Spreadipedia,
        TVGuide,
        Bedheads
    }

    [SerializeField] private AudioClip notificationSound;
    [SerializeField] private AudioSource playerAudio;

    [Header("Clock")]
    [SerializeField] private List<TextMeshProUGUI> phoneClocks;
    [SerializeField] private TextMeshProUGUI worldClock;

    [Header("Phone Pages")]
    [SerializeField] private RectTransform notificationContent;
    public GameObject notificationPage;
    public GameObject appsPage;
    public GameObject messagePage;
    public GameObject eddybeddyPage;

    [Header("Notifications")]
    public List<Notification> notifications;
    [SerializeField] private GameObject notificationIcon;
    [SerializeField] private GameObject notificationPrefab;
    private const float NOTIFICATIONOFFSET = -75f;
    private const float NOTIFICATIONSPACING = 120f;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        
        foreach(TextMeshProUGUI clock in phoneClocks) clock.text = worldClock.text;
        if (notificationPage.activeSelf) UpdateNotificationScreen();
    }

    public void TogglePhone()
    {
        gameObject.SetActive(!gameObject.activeSelf);
        notificationPage.SetActive(true);
        appsPage.SetActive(false);
        messagePage.SetActive(false);
        eddybeddyPage.SetActive(false);
    }

    public void UpdateNotificationIcon()
    {
        if (notifications.Count > 0)
        {
            notificationIcon.SetActive(true);
            notificationIcon.GetComponentInChildren<TextMeshProUGUI>().text = notifications.Count.ToString();
        }
        else
            notificationIcon.SetActive(false);
    }

    private void UpdateNotificationScreen()
    {
        if (notifications.Count == 0) return;

        float yVal = NOTIFICATIONOFFSET;
        foreach (Notification n in notifications)
        {
            int index = notifications.IndexOf(n);

            n.gameObject.SetActive(true);
            if (index > 0) yVal -= NOTIFICATIONSPACING;
            n.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, yVal, 0);

        }

        float contentMax = Mathf.Abs(NOTIFICATIONOFFSET * notifications.Count + 40f);
        notificationContent.sizeDelta = new Vector2(notificationContent.sizeDelta.x, contentMax);
    }

    public void AddNotification(PhoneApp app, string message)
    {
        Notification notification = Instantiate(notificationPrefab, notificationContent).GetComponent<Notification>();
        notification.appsource = app;
        notification.messageBody = message;
        notification.backgroundColor = Color.white;
        notification.UpdateNotification();

        notifications.Add(notification);

        playerAudio.PlayOneShot(notificationSound);
    }

}
