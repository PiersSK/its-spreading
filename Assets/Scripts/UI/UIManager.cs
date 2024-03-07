using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PhoneUI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance {  get; private set; }

    [SerializeField] private GameObject pauseUI;

    private void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        PhoneUI.Instance.UpdateNotificationIcon();
        ThoughtBubble.Instance.UpdateThoughtQueue();
    }

    public void TogglePauseUI()
    {
        pauseUI.SetActive(!pauseUI.activeSelf);
    }

}
