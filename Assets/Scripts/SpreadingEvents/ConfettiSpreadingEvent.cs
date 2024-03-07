using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ConfettiSpreadingEvent : SpreadingEvent
{
    [SerializeField] private List<Room> rooms;
    private bool requirementMet = false;
    private bool triggerEvent = false;

    private const string CONFETTITHOUGHT = "God, the confetti is really getting everywhere It's... Oh";

    private int confettiRooms;
    private void Start()
    {
         ThoughtBubble.Instance.ThoughtBubbleDisplayed += StartConffettiCountdown;
    }


    private void StartConffettiCountdown(object sender, ThoughtBubble.ThoughBubbleDisplayedEventArgs e)
    {
        if (e.thoughtText == CONFETTITHOUGHT)
        {
            Invoke(nameof(SetTriggerEvent), 3f);
        }
    }

    protected override void Update()
    {
        base.Update();
        if(!requirementMet)
        {
            confettiRooms = roomsWithConfetti.Count;
            foreach (Room room in rooms)
            {
                if (room.confettiInRoom > 0 && !roomsWithConfetti.Contains(room.name))
                {
                    roomsWithConfetti.Add(room.name);
                    confettiRooms++;
                    Debug.Log("Number of rooms you've confetti'd:" + confettiRooms);
                }
            }

            if(confettiRooms == rooms.Count && !eventComplete)
            {
                requirementMet = true;
                ThoughtBubble.Instance.ShowThought(PlayerThoughts.ConfettiHasSpread);
            }
        }
    }

    private void SetTriggerEvent()
    {
        triggerEvent = true;
    }

    protected override bool ShouldEventTrigger()
    {
        return triggerEvent;
    }
}
