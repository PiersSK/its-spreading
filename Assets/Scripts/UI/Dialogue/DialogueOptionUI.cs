using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueOptionUI : MonoBehaviour
{
    [SerializeField] private int nextDialogueIndex = 0;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(MoveToNextConversation);
    }

    private void MoveToNextConversation()
    {
        DialogueUI.Instance.LoadCurrentConversationIndex(nextDialogueIndex);
    }

    public void UpdateOption(NPCDialogue.PlayerResponse response)
    {
        Debug.Log("Setting next convo to " + response.outcomeId);
        if (response.outcomeId.HasValue)
        {
            nextDialogueIndex = response.outcomeId.Value;
        }
        GetComponentInChildren<TextMeshProUGUI>().text = response.text;
    }

    //public class PlayerResponse
    //{
    //    public string text;
    //    public int? outcomeId;
    //    public string requirement;
    //    public string outcomeFunction;
    //}
}
