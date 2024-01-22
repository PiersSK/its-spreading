using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueResponse
{
    public DialogueResponse()
    {
    }

    public void CloseDialogue()
    {
        DialogueUI.Instance.gameObject.SetActive(false);
    }
}
