using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObjectiveController : MonoBehaviour, IDataPersistence
{
    public static ObjectiveController Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI scoreDisplay;
    [SerializeField] private TextMeshProUGUI historicStatDisplay;
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

    public void LoadData(GameData data)
    {
        historicStatDisplay.text = "Total spreads found ever: " + data.spreadEventsTriggered;
    }
    public void SaveData(ref GameData data)
    {
        data.spreadEventsTriggered += objectivesComplete;
    }
}
