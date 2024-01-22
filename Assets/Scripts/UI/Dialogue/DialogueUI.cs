using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour
{
    public Image npcPortrait;
    public TextMeshProUGUI npcName;

    public TextMeshProUGUI npcDialogue;
    public List<GameObject> dialogueOptions;

    public TextAsset testDialogue;

    private void Start()
    {
        LoadJsonConversationToUI();
    }

    public void LoadJsonConversationToUI()
    {
        NPCDialogue npcDialogue = JsonUtility.FromJson<NPCDialogue>(testDialogue.text);
        npcPortrait.sprite = Resources.Load<Sprite>("CharacterPortraits/" + npcDialogue.picture);
        npcName.text = npcDialogue.name;


        Debug.Log(npcDialogue.conversations[0].dialogues[0]);

        Conversation conversation = npcDialogue.conversations[0];
        DialogueItem dialogue = conversation.dialogues[0];

        this.npcDialogue.text = dialogue.prompt;

        for(int i = 0; i < dialogueOptions.Count; i++)
        {
            dialogueOptions[i].SetActive(i < dialogue.responses.Length);
            if(i < dialogue.responses.Length)
                dialogueOptions[i].GetComponentInChildren<TextMeshProUGUI>().text = dialogue.responses[i].text;
        }
    }
}
