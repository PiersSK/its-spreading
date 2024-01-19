using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObjectiveController : MonoBehaviour
{
    public static ObjectiveController Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI scoreDisplay;
    [SerializeField] private int totalObjectives = 10;
    private int objectivesComplete = 0;


    public int ObjectivesComplete
    {
        get { return objectivesComplete; }
        set
        {
            objectivesComplete = value;
            scoreDisplay.text = objectivesComplete.ToString() + "/" + totalObjectives.ToString();
        }
    }

    private void Awake()
    {
        Instance = this;
    }
}
