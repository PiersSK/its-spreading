using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueResponse
{
    public DialogueResponse()
    {
    }

    public virtual void CloseDialogue()
    {
        DialogueUI.Instance.gameObject.SetActive(false);
        TimeController.Instance.ToggleTimePause();
    }
}
