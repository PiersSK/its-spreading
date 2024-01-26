using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SisResponse : DialogueResponse
{
    private const string FIRSTTHOUGHT = "Those tickets sounded important to her, she shouldn't miss out.";
    public void CloseWithSuccess()
    {
        LoveSpreadingEvent.Instance.calledSis = true;
        LoveSpreadingEvent.Instance.wasKindToSis = true;
        Player.Instance.TogglePlayerIsEngaged();
        CloseDialogue();
        ThoughtBubble.Instance.ShowThought(FIRSTTHOUGHT);
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
