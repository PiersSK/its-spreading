using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int spreadEventsTriggered;
    public int daysComplete;

    // default values for when the game starts with no data.
    public GameData()
    {
        this.spreadEventsTriggered = 0;
        this.daysComplete = 0;
    }
}
