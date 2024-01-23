using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int spreadEventsTriggered;
    public int daysComplete;
    public float currentTime;
    public Vector3 playerPosition;

    public List<string> completeEvents;

    // default values for when the game starts with no data.
    public GameData()
    {
        this.spreadEventsTriggered = 0;
        this.daysComplete = 0;
        this.playerPosition = new Vector3 (0, 1.1f, 0);
        this.currentTime = 8;
        this.completeEvents = new List<string>();
    }
}
