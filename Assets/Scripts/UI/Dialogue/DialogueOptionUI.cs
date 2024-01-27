using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueOptionUI : MonoBehaviour
{
    [SerializeField] private bool progressesDialogue = true;
    [SerializeField] private int nextDialogueIndex = 0;
    [SerializeField] private string outcomeFunction;

    public DialogueResponse responseClass;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(MoveToNextConversation);
        outcomeFunction = string.Empty;
    }

    private void MoveToNextConversation()
    {
        if(outcomeFunction != string.Empty)
        {
            var type = responseClass.GetType();
            var method = type.GetMethod(outcomeFunction);
            method.Invoke(responseClass, null);
        }
        if(progressesDialogue) DialogueUI.Instance.LoadCurrentConversationIndex(nextDialogueIndex);
    }

    public void UpdateOption(NPCDialogue.PlayerResponse response)
    {
        progressesDialogue = response.outcomeId.HasValue;
        if (progressesDialogue) nextDialogueIndex = response.outcomeId.Value;

        outcomeFunction = response.outcomeFunction != null ? response.outcomeFunction : string.Empty;

        GetComponentInChildren<TextMeshProUGUI>().text = response.text;
    }
}
