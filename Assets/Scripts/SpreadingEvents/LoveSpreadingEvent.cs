using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class LoveSpreadingEvent : SpreadingEvent
{
    public static LoveSpreadingEvent Instance { get; private set; }

    public bool calledSisNoSuccess = false;
    public bool calledSis = false;
    public bool wasKindToSis = false;
    public bool bookedTickets = false;
    public bool gaveTicketsToSis = false;

    private bool madeSisAvailable = false;
    [SerializeField] private int sisAvailableHour = 18;
    [SerializeField] private GameObject callSisBlock;
    [SerializeField] private Button callSisButton;
    [SerializeField] private DialogueNPC sisNPC;


    private void Awake()
    {
        Instance = this;
    }

    protected override void Update()
    {
        base.Update();

        if (calledSis && wasKindToSis && bookedTickets && !madeSisAvailable
            && TimeController.Instance.TimeHasPassed(sisAvailableHour, 0))
        {
            callSisBlock.SetActive(true);

            callSisButton.onClick.RemoveAllListeners();
            callSisButton.onClick.AddListener(CallSis);
            madeSisAvailable = true;
        }
    }

    protected override bool ShouldEventTrigger()
    {
        return gaveTicketsToSis;
    }

    private void CallSis()
    {
        TimeController.Instance.ToggleTimePause();
        Player.Instance.LockPlayerIfNotEngaged(true);
        DialogueUI.Instance.LoadJsonConversationToUI(sisNPC.dialogueFile, sisNPC, 1);
        PhoneUI.Instance.TogglePhone();

        callSisBlock.SetActive(false);
    }
}
