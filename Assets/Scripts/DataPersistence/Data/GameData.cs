using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int spreadEventsTriggered;
    public int daysComplete;

    public Vector3 playerPosition;

    // default values for when the game starts with no data.
    public GameData()
    {
        this.spreadEventsTriggered = 0;
        this.daysComplete = 0;
        this.playerPosition = Vector3.zero;
    }
}
