using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReligionSpreadingEvent : SpreadingEvent
{

    [SerializeField] private GameObject pamphletObject;

    private bool playerReceivedPamphlet = false;
    private bool playerGavePamphlet = false;
    private bool hasReadPamphlet = false;

    private const float TIMETILLREAD = 10f;
    private const string READINGREACTION = "This book is interesting, Magic Darren is quite impressive. Maybe I should tell someone else.";

    protected override void Update()
    {
        base.Update();
        if(!hasReadPamphlet && playerReceivedPamphlet)
        {
            hasReadPamphlet = true;
            Invoke(nameof(ReadPamphlet), TIMETILLREAD);
        }
    }

    private void ReadPamphlet()
    {
        TogglePamplet();
        ThoughtBubble.Instance.ShowThought(READINGREACTION);
        Invoke(nameof(TogglePamplet), 4f);
    }

    private void TogglePamplet()
    {
        Player.Instance._animator.SetBool("readingBook", !pamphletObject.activeSelf);
        pamphletObject.SetActive(!pamphletObject.activeSelf);
    }

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

    protected override void EventImpact()
    {
        GossipSpreadingEvent.Instance.hasMetDarren = true;
    }
}
