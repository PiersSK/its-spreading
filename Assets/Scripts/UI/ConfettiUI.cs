using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConfettiUI : MonoBehaviour
{
    public static ConfettiUI Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI spreadingTitle;
    [SerializeField] private float showTitleFor;
    
    private float titleTimer = 0f;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (titleTimer > 0f)
        {
            titleTimer -= Time.deltaTime;
            Color titleColor = spreadingTitle.color;
            titleColor.a = titleTimer / showTitleFor;
            spreadingTitle.color = titleColor;
        } else
        {
            titleTimer = 0f;
        }
    }

    public void ShowItsSpreading()
    {
        titleTimer = showTitleFor;
    }
}
