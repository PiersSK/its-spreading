using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    public static StartGame Instance { get; private set; }

    [SerializeField] private Vector3 startPosition;
    private ThoughtBubble _thoughtBubble;

    private List<SpreadingEvent> spreadingEvents;

    private int spreadEventsFound = 0;

    private const string WAKEUPTHOUGHT = "Mmmm finally a day off, let's start the day with some lovely jam on toast";
    private const string FIRSTSPREADTHOUGHT = "Oh my! That shouldn't happen here... I should spread again to be sure...";
    private const string SECONDSPREADTHOUGHT = "Oh bother that confirms it. I got doused with too many spreadheads at work. I guess I need to get spreading, otherwise that's gonna be a nuisance";

    private void Awake()
    {
        Instance = this;
    }

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
        else if (spreadEventsFound == 1)
            _thoughtBubble.ShowThought(SECONDSPREADTHOUGHT, 7f);
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
