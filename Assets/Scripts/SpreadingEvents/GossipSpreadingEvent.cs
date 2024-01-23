using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GossipSpreadingEvent : SpreadingEvent
{
    public static GossipSpreadingEvent Instance {  get; private set; }

    [SerializeField] private Button horsespiracyButton;
    [SerializeField] private Button millyPeadButton;

    public bool hasMetDarren = false;
    public bool hasReadHorsespiracy = false;
    public bool hasReadMillyPead = false;

    public bool hasSpreadGossip = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        horsespiracyButton.onClick.AddListener(() => { hasReadHorsespiracy = true; });
        millyPeadButton.onClick.AddListener(() => { hasReadMillyPead = true; });
    }

    protected override bool ShouldEventTrigger()
    {
        return hasSpreadGossip;
    }

    public bool PlayerHasGossip()
    {
        return hasMetDarren || hasReadHorsespiracy || hasReadMillyPead;
    }
}
