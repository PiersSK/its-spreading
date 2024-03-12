using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Objective : MonoBehaviour
{
    private bool isComplete = false;
    //private bool completedBefore = false; // To implement later
    public AudioClip spreadingVoiceLine;

    [Header("UI References")]
    [SerializeField] private Image objectiveUI;
    [SerializeField] private Sprite completedIcon;

    [Header("Flare")]
    [SerializeField] private string completionThought;
    public string spreadingSubtitle;

    private const float TIMETILLTHOUGHT = 3f;

    public void CompleteObjective(bool haveThought = true)
    {
        objectiveUI.sprite = completedIcon;
        objectiveUI.color = Color.white;
        isComplete = true;
        if(completionThought != string.Empty && haveThought) 
            Invoke(nameof(FollowUpThought), TIMETILLTHOUGHT);
    }

    private void FollowUpThought()
    {
        Debug.Log(completedIcon.name);
        ThoughtBubble.Instance.ShowThought(new ThoughtBubble.Thought(completionThought, PlayerThoughts.ICONFPATH + completedIcon.name, 5f));
    }

}
