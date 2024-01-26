using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DanceClassEvent : LimitedTimedEvent, IDataPersistence
{
    [SerializeField] private Button classButton;
    [SerializeField] private TextMeshProUGUI classButtonText;
    public bool playerSignedUp = false;
    public bool classHasStarted = false;

    public void LoadData(GameData data)
    {
        playerSignedUp = data.isSignedUpToDanceClass;
    }
    public void SaveData(ref GameData data)
    {
        data.isSignedUpToDanceClass = playerSignedUp;
    }
    
    public override void TriggerEvent()
    {
        classButtonText.text = playerSignedUp ? "Join class now!" : "Class in progress, sign up next time!";
        classButton.interactable = playerSignedUp;
        classHasStarted = true;
    }

    public override void TriggerEventEnd()
    {
        classButtonText.text = "Class over! See you next time!";
        classButton.interactable = false;
    }

    
}
