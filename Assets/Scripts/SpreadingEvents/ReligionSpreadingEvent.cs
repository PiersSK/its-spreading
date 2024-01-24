using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReligionSpreadingEvent : SpreadingEvent
{

    [SerializeField] private bool playerReceivedPamphlet = false;
    [SerializeField] private bool playerGavePamphlet = false;
    [SerializeField] private InventoryItemData expectedItem;

    private void Start()
    {
        Player.Instance.GetComponent<InventorySystem>().onInventoryChangeEvent += CheckPamphletState;
    }

    private void CheckPamphletState()
    {
        if (!playerReceivedPamphlet && Player.Instance.GetComponent<InventorySystem>().HasItem(expectedItem)) playerReceivedPamphlet = true;
        if (playerReceivedPamphlet && !playerGavePamphlet && !Player.Instance.GetComponent<InventorySystem>().HasItem(expectedItem)) playerGavePamphlet = true;
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
