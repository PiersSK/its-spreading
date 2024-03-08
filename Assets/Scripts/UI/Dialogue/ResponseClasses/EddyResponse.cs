using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EddyResponse : DialogueResponse
{
    public bool CompletedReligionEvent()
    {
        return GossipSpreadingEvent.Instance.hasMetDarren;
    }

    public bool HasReadHorsespiracy()
    {
        return GossipSpreadingEvent.Instance.hasReadHorsespiracy;
    }

    public bool HasReadMillyPead()
    {
        return GossipSpreadingEvent.Instance.hasReadMillyPead;
    }

    public override void CloseDialogue()
    {
        base.CloseDialogue();
        if (!DialogueUI.Instance.playerEngagedPreConvo)
            Player.Instance.FreePlayerIfEngaged(true, true);
        else
            Player.Instance.UnlockOptionalLockableActions();
        GossipSpreadingEvent.Instance.hasSpreadGossip = true;
    }

    public void CloseWithFailure()
    {
        base.CloseDialogue();
        if (!DialogueUI.Instance.playerEngagedPreConvo)
            Player.Instance.FreePlayerIfEngaged(true, true);
        else
            Player.Instance.UnlockOptionalLockableActions();
    }
}
