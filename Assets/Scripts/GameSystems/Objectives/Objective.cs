using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Objective : MonoBehaviour
{
    private bool isComplete = false;
    //private bool completedBefore = false; // To implement later

    [Header("UI References")]
    [SerializeField] private Image objectiveUI;
    //[SerializeField] private Sprite silhouetteIcon;
    [SerializeField] private Sprite completedIcon;

    public void CompleteObjective()
    {
        objectiveUI.sprite = completedIcon;
        objectiveUI.color = Color.white;
        isComplete = true;
    }

}
