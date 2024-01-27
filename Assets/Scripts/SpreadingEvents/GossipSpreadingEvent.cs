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
    private int gossipReactedTo = 0;

    public bool hasSpreadGossip = false;

    private const string FIRSTTHOUGHT = "Hmm, juicy goss, wonder who I could tell about this.";
    private const string SECONDTHOUGHT = "More juicy goss.";

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        horsespiracyButton.onClick.AddListener(HasSeenHorsespiracy);
        millyPeadButton.onClick.AddListener(HasSeenMillyPead);
    }

    private void HasSeenHorsespiracy()
    {
        hasReadHorsespiracy = true;
        AcknowledgeGossip();
        horsespiracyButton.onClick.RemoveListener(HasSeenHorsespiracy);
    }

    private void HasSeenMillyPead()
    {
        hasReadMillyPead = true;
        AcknowledgeGossip();
        millyPeadButton.onClick.RemoveListener(HasSeenMillyPead);
    }

    private void AcknowledgeGossip()
    {
        if (gossipReactedTo == 0)
            ThoughtBubble.Instance.ShowThought(FIRSTTHOUGHT);
        else if (gossipReactedTo == 1)
            ThoughtBubble.Instance.ShowThought(SECONDTHOUGHT);

        gossipReactedTo++;
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
