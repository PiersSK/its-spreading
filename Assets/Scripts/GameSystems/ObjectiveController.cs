using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObjectiveController : MonoBehaviour, IDataPersistence
{
    public static ObjectiveController Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI scoreDisplay;
    [SerializeField] private TextMeshProUGUI historicStatDisplay;

    public static int totalObjectives = 11;
    public static int objectivesComplete = 0;
    private int objectivesCompleteAllTime = 0;

    private const string HISTSTATPREFIX = "Total spreads found ever: ";


    public int ObjectivesComplete
    {
        get { return objectivesComplete; }
        set
        {
            objectivesComplete = value;
            scoreDisplay.text = objectivesComplete.ToString() + "/" + totalObjectives.ToString();
            historicStatDisplay.text = HISTSTATPREFIX + (objectivesCompleteAllTime + objectivesComplete);
        }
    }

    private void Awake()
    {
        Instance = this;
    }

    public void LoadData(GameData data)
    {
        historicStatDisplay.text = HISTSTATPREFIX + data.spreadEventsTriggered;
        objectivesCompleteAllTime = data.spreadEventsTriggered;
    }
    public void SaveData(ref GameData data)
    {
        data.spreadEventsTriggered += objectivesComplete;
    }
}
