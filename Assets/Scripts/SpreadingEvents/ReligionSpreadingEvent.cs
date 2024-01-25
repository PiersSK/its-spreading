using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReligionSpreadingEvent : SpreadingEvent
{

    [SerializeField] private bool playerReceivedPamphlet = false;
    [SerializeField] private bool playerGavePamphlet = false;
  

    private void Start()
    {
        Player.Instance.newInventory.OnInventoryChanged += CheckPamphletState;
    }

    private void CheckPamphletState(object sender, InventorySystem.OnInventoryChangedEventArgs e)
    {
        if (!playerReceivedPamphlet && e.itemAdded == requiredItemName) playerReceivedPamphlet = true;
        if (playerReceivedPamphlet && !playerGavePamphlet && e.itemRemoved == requiredItemName) playerGavePamphlet = true;
    }

    protected override bool ShouldEventTrigger()
    {
        return playerGavePamphlet && Player.Instance.isUnengaged;
    }

    protected override void EventImpact()
    {
        GossipSpreadingEvent.Instance.hasMetDarren = true;
    }
}
