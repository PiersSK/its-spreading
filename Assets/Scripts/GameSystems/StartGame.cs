using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    [SerializeField] private Vector3 startPosition;
    private ThoughtBubble _thoughtBubble;

    private List<SpreadingEvent> spreadingEvents;

    private bool isLoadingFromResume = false;
    private int spreadEventsFound = 0;

    private void Start()
    {
        _thoughtBubble = ThoughtBubble.Instance;
        spreadingEvents = FindObjectsOfType<SpreadingEvent>().ToList();

        if (!isLoadingFromResume)
        {
            Player.Instance.ForcePlayerToPosition(startPosition);
            Player.Instance.TogglePlayerIsEngaged(true);
            Player.Instance._animator.SetTrigger("stretch");
            CameraController.Instance.SetCameraZoom(5f, 0.5f);
            Invoke(nameof(TutorialThought), 4f);

            foreach(SpreadingEvent spreadingEvent in spreadingEvents)
            {
                spreadingEvent.SpreadEventComplete += TutorialThoughts;
            }
        }
    }

    private void TutorialThoughts(object sender, System.EventArgs e)
    {
        if (spreadEventsFound == 0)
            _thoughtBubble.ShowThought("Oh god! That shouldn't happen here... I should spread again to be sure...");
        else if (spreadEventsFound == 1)
            _thoughtBubble.ShowThought("Oh bother that confirms it. I got hit with too many spreadheads at work. I guess I need to get spreading, otherwise that's gonna be a nuisance", 7f);
        else
        {
            foreach (SpreadingEvent spreadingEvent in spreadingEvents)
            {
                spreadingEvent.SpreadEventComplete -= TutorialThoughts;
            }
        }

        spreadEventsFound++;
    }

    private void TutorialThought()
    {
        CameraController.Instance.SetCameraZoom(9f, 0.2f);
        Player.Instance.TogglePlayerIsEngaged();
        _thoughtBubble.ShowThought("Mmmm finally a day off, let's start the day with some lovely jam on toast");
    }
}
