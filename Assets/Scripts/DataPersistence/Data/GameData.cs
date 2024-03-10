using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public bool dayIsComplete;
    public int spreadEventsTriggered;
    public int daysComplete;
    public Vector3 playerPosition;
    public List<string> completeEvents;
    public int numCompleteEvents;
    public int currentHour;
    public int currentMinute;
    public bool fireStarted;
    public bool isSignedUpToDanceClass;
    public List<string> inventory;
    public bool hasNeighbourEventTriggered;
    public bool hasReceivedDelivery;
    public Vines.PlantState plantState;
    public bool hasDarrenGossip;
    public bool hasReadHorsepiracy;
    public bool hasReadMillyPead;

    public List<string> roomsWithConfetti;
    public bool calledSis;
    public bool bookedTickets;
    public bool gaveTicketsToSis;
    public bool calledSisNoSuccess;
    public bool wasKindToSis;
    public bool breadIsFlipped;
    public bool petriInFridge;
    public bool petriReminderSent;
    public bool postRottenInteration;
    public bool hasTalkedToEddy;
    public bool hasReactedToRottenFridge;

    // default values for when the game starts with no data.
    public GameData()
    {
        dayIsComplete = false;
        spreadEventsTriggered = 0;
        daysComplete = 0;
        playerPosition = new Vector3 (0, 1.1f, 0);
        completeEvents = new List<string>();
        numCompleteEvents = 0;
        currentHour = 8;
        currentMinute = 0;
        fireStarted = false;
        isSignedUpToDanceClass = false;
        hasNeighbourEventTriggered = false;
        hasReceivedDelivery = false;
        hasDarrenGossip = false;
        hasReadHorsepiracy = false;
        hasReadMillyPead = false;
        roomsWithConfetti = new();
        calledSis = false;
        bookedTickets = false;
        gaveTicketsToSis = false;
        calledSisNoSuccess = false;
        wasKindToSis = false;
        hasTalkedToEddy = false;
        inventory = new();
        breadIsFlipped = false;
        petriInFridge = false;
        petriReminderSent = false;
        postRottenInteration = false;
        hasReactedToRottenFridge = false;
    }

    public GameData(int spreadEventsTriggered, int daysComplete)
    {
        dayIsComplete = false;
        this.spreadEventsTriggered = spreadEventsTriggered;
        this.daysComplete = daysComplete;
        playerPosition = new Vector3(0, 1.1f, 0);
        completeEvents = new List<string>();
        numCompleteEvents = 0;
        currentHour = 8;
        currentMinute = 0;
        fireStarted = false;
        isSignedUpToDanceClass = false;
        hasNeighbourEventTriggered = false;
        hasReceivedDelivery = false;
        hasDarrenGossip = false;
        hasReadHorsepiracy = false;
        hasReadMillyPead = false;
        roomsWithConfetti = new();
        calledSis = false;
        bookedTickets = false;
        gaveTicketsToSis = false;
        calledSisNoSuccess = false;
        wasKindToSis = false;
        hasTalkedToEddy = false;
        hasReactedToRottenFridge = false;
        inventory = new();
    }
}
