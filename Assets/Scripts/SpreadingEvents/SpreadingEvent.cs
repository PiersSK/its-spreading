using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadingEvent : MonoBehaviour, IDataPersistence
{
    [SerializeField] Objective _objective;
    protected bool eventComplete = false;
    public List<string> completeEvents = new List<string>();

    public void LoadData(GameData data)
    {
        this.completeEvents = data.completeEvents;
        GossipSpreadingEvent.Instance.hasMetDarren = data.hasDarrenGossip;
        GossipSpreadingEvent.Instance.hasReadHorsespiracy = data.hasReadHorsepiracy;
        GossipSpreadingEvent.Instance.hasReadMillyPead = data.hasReadMillyPead;
    }

    public void SaveData(ref GameData data)
    {
        data.completeEvents = this.completeEvents;
        data.hasDarrenGossip = GossipSpreadingEvent.Instance.hasMetDarren;
        data.hasReadHorsepiracy = GossipSpreadingEvent.Instance.hasReadHorsespiracy;
        data.hasReadMillyPead = GossipSpreadingEvent.Instance.hasReadMillyPead;
    }


    protected virtual void Update()
    {
        foreach(string name  in completeEvents)
        {
            if(_objective.gameObject.name == name)
            {
                eventComplete = true;
                _objective.CompleteObjective();
                EventImpact();
            }
        }
        if (ShouldEventTrigger() && !eventComplete)
        {
            eventComplete = true;
            Confetti.Instance.ConfettiExplosion(_objective.spreadingVoiceLine);
            _objective.CompleteObjective();
            ObjectiveController.Instance.ObjectivesComplete++;
            completeEvents.Add(_objective.gameObject.name);
            EventImpact();
        }


    }

    protected virtual bool ShouldEventTrigger()
    {
        return false;
    }

    protected virtual void EventImpact() { }

}
