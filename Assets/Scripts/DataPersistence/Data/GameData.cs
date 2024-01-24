using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int spreadEventsTriggered;
    public int daysComplete;
    public Vector3 playerPosition;
    public List<string> completeEvents;
    public int numCompleteEvents;
    public int currentHour;
    public int currentMinute;
    public bool fireStarted;
    public bool isSignedUpToDanceClass;
    public List<Inventory.InventoryItem> inventory;
    public bool hasNeighbourEventTriggered;
    
    // default values for when the game starts with no data.
    public GameData()
    {
        this.spreadEventsTriggered = 0;
        this.daysComplete = 0;
        this.playerPosition = new Vector3 (0, 1.1f, 0);
        this.completeEvents = new List<string>();
        this.numCompleteEvents = 0;
        this.currentHour = 8;
        this.currentMinute = 0;
        this.fireStarted = false;
        this.isSignedUpToDanceClass = false;
        this.hasNeighbourEventTriggered = false;
    }
}
