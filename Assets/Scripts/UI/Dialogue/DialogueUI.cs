using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;

public class DialogueUI : MonoBehaviour
{
    public static DialogueUI Instance { get; private set; }

    public Image npcPortrait;
    public TextMeshProUGUI npcName;

    public TextMeshProUGUI npcDialoguePrompt;
    public List<DialogueOptionUI> dialogueOptions;

    public TextAsset testDialogue;

    public NPCDialogue loadedNPCDialogue;
    public int currentConversationIndex = 0;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        LoadJsonConversationToUI();
    }

    public void LoadJsonConversationToUI()
    {
        loadedNPCDialogue = JsonConvert.DeserializeObject<NPCDialogue>(testDialogue.text);
        npcPortrait.sprite = Resources.Load<Sprite>("CharacterPortraits/" + loadedNPCDialogue.picture);
        npcName.text = loadedNPCDialogue.name;

        LoadCurrentConversationIndex(0);
    }

    public void LoadCurrentConversationIndex(int index)
    {
        NPCDialogue.Conversation conversation = loadedNPCDialogue.conversations[currentConversationIndex];
        NPCDialogue.DialogueItem dialogue = conversation.dialogues[index];

        npcDialoguePrompt.text = dialogue.prompt;

        for (int i = 0; i < dialogueOptions.Count; i++)
        {
            dialogueOptions[i].gameObject.SetActive(i < dialogue.responses.Length);
            if (i < dialogue.responses.Length)
                dialogueOptions[i].UpdateOption(dialogue.responses[i]);
        }
    }
}
