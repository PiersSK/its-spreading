using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObjectiveController : MonoBehaviour, IDataPersistence
{
    public static ObjectiveController Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI scoreDisplay;
    [SerializeField] private TextMeshProUGUI historicStatDisplay;
    [SerializeField] private Transform objectiveParent;

    public int totalObjectives = 11;
    public int objectivesComplete = 0;
    private int objectivesCompleteAllTime = 0;

    public static bool allObjectivesComplete = false;

    private const string HISTSTATPREFIX = "Total spreads found ever: ";
    private const string FINALTHOUGHT = "Hey! I think I've finally spread enough to detox, maybe it's time to go to bed!";


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

    private void Start()
    {
        allObjectivesComplete = false;
        totalObjectives = objectiveParent.childCount;
        scoreDisplay.text = objectivesComplete.ToString() + "/" + totalObjectives.ToString();
        historicStatDisplay.text = HISTSTATPREFIX + (objectivesCompleteAllTime + objectivesComplete);
    }

    private void Update()
    {
        if(!allObjectivesComplete)
        {
            if(objectivesComplete == totalObjectives)
            {
                allObjectivesComplete = true;
                ThoughtBubble.Instance.ShowThought(PlayerThoughts.LastSpread);
            }
        }
    }

    public void LoadData(GameData data)
    {
        historicStatDisplay.text = HISTSTATPREFIX + data.spreadEventsTriggered;
        objectivesCompleteAllTime = data.spreadEventsTriggered;
        ObjectivesComplete = data.numCompleteEvents;
    }
    public void SaveData(ref GameData data)
    {
        data.spreadEventsTriggered += objectivesComplete;
        data.numCompleteEvents = ObjectivesComplete;
    }
}
