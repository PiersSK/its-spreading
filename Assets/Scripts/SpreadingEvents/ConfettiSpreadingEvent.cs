using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfettiSpreadingEvent : SpreadingEvent
{
    [SerializeField] private List<Room> rooms;
    private bool requirementMet = false;
    private bool triggerEvent = false;

    private const string CONFETTITHOUGHT = "God, the confetti is really getting everywhere It's... Oh";

    protected override void Update()
    {
        base.Update();
        if(!requirementMet)
        {
            int confettiRooms = 0;

            foreach (Room room in rooms)
            {
                if (room.confettiInRoom > 0) confettiRooms++;
            }

            if(confettiRooms == rooms.Count)
            {
                requirementMet = true;
                ThoughtBubble.Instance.ShowThought(CONFETTITHOUGHT);
                Invoke(nameof(SetTriggerEvent), 5f);
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
