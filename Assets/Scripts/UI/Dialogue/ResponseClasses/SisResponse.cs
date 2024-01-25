using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SisResponse : DialogueResponse
{
    public void CloseWithSuccess()
    {
        LoveSpreadingEvent.Instance.calledSis = true;
        LoveSpreadingEvent.Instance.calledSisNoSuccess = false;
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
