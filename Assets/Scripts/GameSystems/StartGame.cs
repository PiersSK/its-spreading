using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StartGame : MonoBehaviour, IDataPersistence
{
    public static StartGame Instance { get; private set; }

    [SerializeField] private Vector3 startPosition;
    private ThoughtBubble _thoughtBubble;

    private List<SpreadingEvent> spreadingEvents;

    private int spreadEventsFound = 0;

    private const string WAKEUPTHOUGHT = "Mmmm finally a day off, let's start the day with some lovely jam on toast";
    private const string FIRSTSPREADTHOUGHT = "Oh my! That shouldn't happen here... I must've been doused with too many spreadheads at work. I need to spread 11 things by midnight to tire them out";

    private void Awake()
    {
        Instance = this;
    }

    public void LoadData(GameData data)
    {
        spreadEventsFound = data.numCompleteEvents;
    }

    public void SaveData(ref GameData data) {}

    private void Start()
    {
        _thoughtBubble = ThoughtBubble.Instance;
        spreadingEvents = FindObjectsOfType<SpreadingEvent>().ToList();
    }

    public void StartDayFresh()
    {
        Player.Instance.ForcePlayerToPosition(startPosition);
        Player.Instance.TogglePlayerIsEngaged(true);
        Player.Instance._animator.SetTrigger("stretch");
        CameraController.Instance.SetCameraZoom(5f, 0.5f);
        Invoke(nameof(WakeupThought), 4f);

        foreach(SpreadingEvent spreadingEvent in spreadingEvents)
        {
            spreadingEvent.SpreadEventComplete += TutorialThoughts;
        }
    }

    private void TutorialThoughts(object sender, System.EventArgs e)
    {
        if (spreadEventsFound == 0)
            _thoughtBubble.ShowThought(FIRSTSPREADTHOUGHT);
        else
        {
            foreach (SpreadingEvent spreadingEvent in spreadingEvents)
            {
                spreadingEvent.SpreadEventComplete -= TutorialThoughts;
            }
        }

        spreadEventsFound++;
    }

    private void WakeupThought()
    {
        CameraController.Instance.SetCameraZoom(9f, 0.2f);
        Player.Instance.TogglePlayerIsEngaged();
        _thoughtBubble.ShowThought(WAKEUPTHOUGHT);
    }
}
