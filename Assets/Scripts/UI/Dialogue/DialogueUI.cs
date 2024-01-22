using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using System;

public class DialogueUI : MonoBehaviour
{
    public static DialogueUI Instance { get; private set; }

    [Header("UI References")]
    [SerializeField] private Image npcPortrait;
    [SerializeField] private TextMeshProUGUI npcName;
    [SerializeField] private TextMeshProUGUI npcDialoguePrompt;
    [SerializeField] private List<DialogueOptionUI> dialogueOptions;

    private NPCDialogue loadedNPCDialogue;
    private int currentConversationIndex = 0;
    public NPC currentNPC;

    private const string PORTRAITFOLDER = "CharacterPortraits/";

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void LoadJsonConversationToUI(TextAsset dialogueFile, NPC npcTalking, int conversationIndex = 0)
    {
        currentNPC = npcTalking;

        loadedNPCDialogue = JsonConvert.DeserializeObject<NPCDialogue>(dialogueFile.text);
        npcPortrait.sprite = Resources.Load<Sprite>(PORTRAITFOLDER + loadedNPCDialogue.picture);
        npcName.text = loadedNPCDialogue.name;

        currentConversationIndex = conversationIndex;
        LoadCurrentConversationIndex(conversationIndex);
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
            {
                var type = Type.GetType(loadedNPCDialogue.responseClass);
                dialogueOptions[i].responseClass = (DialogueResponse)Activator.CreateInstance(type);
                dialogueOptions[i].UpdateOption(dialogue.responses[i]);
            }
        }
    }
}
