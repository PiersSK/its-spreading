using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SisResponse : DialogueResponse
{
    public void CloseWithSuccess()
    {
        LoveSpreadingEvent.Instance.calledSis = true;
        LoveSpreadingEvent.Instance.calledSisNoSuccess = false;
        LoveSpreadingEvent.Instance.wasKindToSis = true;
        if (!DialogueUI.Instance.playerEngagedPreConvo)
            Player.Instance.FreePlayerIfEngaged(true, true);
        else
            Player.Instance.UnlockOptionalLockableActions();
        CloseDialogue();
        ThoughtBubble.Instance.ShowThought(PlayerThoughts.SuccessfulSisCall);
    }

    public void CloseWithFailure()
    {
        LoveSpreadingEvent.Instance.calledSis = true;
        LoveSpreadingEvent.Instance.wasKindToSis = false;
        if (!DialogueUI.Instance.playerEngagedPreConvo)
            Player.Instance.FreePlayerIfEngaged(true, true);
        else
            Player.Instance.UnlockOptionalLockableActions();
        CloseDialogue();
    }

    public void CloseWithSpread()
    {
        LoveSpreadingEvent.Instance.gaveTicketsToSis = true;
        if (!DialogueUI.Instance.playerEngagedPreConvo)
            Player.Instance.FreePlayerIfEngaged(true, true);
        else
            Player.Instance.UnlockOptionalLockableActions();
        CloseDialogue();
    }
}
