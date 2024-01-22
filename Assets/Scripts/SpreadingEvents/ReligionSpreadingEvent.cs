using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReligionSpreadingEvent : SpreadingEvent
{

    [SerializeField] private bool playerReceivedPamphlet = false;
    [SerializeField] private bool playerGavePamphlet = false;

    private void Start()
    {
        Player.Instance._inventory.OnInventoryChanged += CheckPamphletState;
    }

    private void CheckPamphletState(object sender, Inventory.OnInventoryChangedEventArgs e)
    {
        if (!playerReceivedPamphlet && e.itemAdded == Inventory.InventoryItem.DarrenPamphlet) playerReceivedPamphlet = true;
        if (playerReceivedPamphlet && !playerGavePamphlet && e.itemRemoved == Inventory.InventoryItem.DarrenPamphlet) playerGavePamphlet = true;
    }

    protected override bool ShouldEventTrigger()
    {
        return playerGavePamphlet && Player.Instance.isUnengaged;
    }
}
