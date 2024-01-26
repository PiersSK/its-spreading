using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadingEvent : MonoBehaviour, IDataPersistence
{
    public event EventHandler<EventArgs> SpreadEventComplete;
    [SerializeField] Objective _objective;
    [SerializeField] public string requiredItemName;
    protected bool eventComplete = false;
    public List<string> completeEvents = new();

    public List<string> roomsWithConfetti = new();

    public void LoadData(GameData data)
    {
        this.completeEvents = data.completeEvents;
        GossipSpreadingEvent.Instance.hasMetDarren = data.hasDarrenGossip;
        GossipSpreadingEvent.Instance.hasReadHorsespiracy = data.hasReadHorsepiracy;
        GossipSpreadingEvent.Instance.hasReadMillyPead = data.hasReadMillyPead;
        roomsWithConfetti = data.roomsWithConfetti;
        LoveSpreadingEvent.Instance.calledSis = data.calledSis;
        LoveSpreadingEvent.Instance.bookedTickets = data.bookedTickets;
        LoveSpreadingEvent.Instance.gaveTicketsToSis = data.gaveTicketsToSis;
        LoveSpreadingEvent.Instance.calledSisNoSuccess = data.calledSisNoSuccess;
        LoveSpreadingEvent.Instance.wasKindToSis = data.wasKindToSis;
        foreach(string name  in completeEvents)
        {
            if(_objective.gameObject.name == name)
            {
                eventComplete = true;
                _objective.CompleteObjective(false);
                EventImpact();
            }
        }
    }

    public void SaveData(ref GameData data)
    {
        data.completeEvents = this.completeEvents;
        data.hasDarrenGossip = GossipSpreadingEvent.Instance.hasMetDarren;
        data.hasReadHorsepiracy = GossipSpreadingEvent.Instance.hasReadHorsespiracy;
        data.hasReadMillyPead = GossipSpreadingEvent.Instance.hasReadMillyPead;
        data.roomsWithConfetti = roomsWithConfetti;
        data.calledSis = LoveSpreadingEvent.Instance.calledSis;
        data.bookedTickets = LoveSpreadingEvent.Instance.bookedTickets;
        data.gaveTicketsToSis = LoveSpreadingEvent.Instance.gaveTicketsToSis;
        data.calledSisNoSuccess = LoveSpreadingEvent.Instance.calledSisNoSuccess;
        data.wasKindToSis = LoveSpreadingEvent.Instance.wasKindToSis;
    }


    protected virtual void Update()
    {
        
        if (ShouldEventTrigger() && !eventComplete)
        {
            eventComplete = true;
            Confetti.Instance.ConfettiExplosion(_objective.spreadingVoiceLine, _objective.spreadingSubtitle);
            _objective.CompleteObjective();
            ObjectiveController.Instance.ObjectivesComplete++;
            completeEvents.Add(_objective.gameObject.name);
            EventImpact();
            SpreadEventComplete?.Invoke(this, EventArgs.Empty);
        }


    }

    protected virtual bool ShouldEventTrigger()
    {
        return false;
    }

    protected virtual void EventImpact() { }

}
