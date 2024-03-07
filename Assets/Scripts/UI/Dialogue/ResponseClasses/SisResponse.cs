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
        Player.Instance.TogglePlayerIsEngaged();
        CloseDialogue();
        ThoughtBubble.Instance.ShowThought(PlayerThoughts.SuccessfulSisCall);
    }

    public void CloseWithFailure()
    {
        LoveSpreadingEvent.Instance.calledSis = true;
        LoveSpreadingEvent.Instance.wasKindToSis = false;
        Player.Instance.TogglePlayerIsEngaged();
        CloseDialogue();
    }

    public void CloseWithSpread()
    {
        LoveSpreadingEvent.Instance.gaveTicketsToSis = true;
        Player.Instance.TogglePlayerIsEngaged();
        CloseDialogue();
    }
}
