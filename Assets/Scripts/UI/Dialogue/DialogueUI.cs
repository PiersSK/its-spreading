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
    [Header("UI Settings")]
    [Range(1000, 1920)]
    [SerializeField] private float maxResponseUIWidth;
    [Range(0,1)]
    [SerializeField] private float responseUIPadding;

    [Header("UI References")]
    [SerializeField] private Image npcPortrait;
    [SerializeField] private Image npcNameplate;
    [SerializeField] private TextMeshProUGUI npcName;
    [SerializeField] private DialogueTypewriter npcDialoguePrompt;
    [SerializeField] private GameObject optionsParent;
    [SerializeField] private List<DialogueOptionUI> dialogueOptions;

    [Header("UI Elements To Hide")]
    [SerializeField] private GameObject gameplayUI;
    [SerializeField] private GameObject inventoryUI;
    [SerializeField] private GameObject phonePrompt;

    private NPCDialogue loadedNPCDialogue;
    private int currentConversationIndex = 0;
    [Header("Debug")]
    public NPC currentNPC;
    public bool playerEngagedPreConvo = false;

    private const string PORTRAITFOLDER = "CharacterPortraits/";

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        gameplayUI.SetActive(false);
        inventoryUI.SetActive(false);
        phonePrompt.SetActive(false);
    }

    private void OnDisable()
    {
        gameplayUI.SetActive(true);
        inventoryUI.SetActive(true);
        phonePrompt.SetActive(true);
    }

    public void StartConversation(TextAsset dialogueFile, NPC npcTalking, int conversationIndex = 0)
    {
        playerEngagedPreConvo = !Player.Instance.isUnengaged;

        TimeController.Instance.ToggleTimePause();
        Player.Instance.LockPlayerIfNotEngaged(true, true);

        if (playerEngagedPreConvo)
            Player.Instance.LockOptionalLockableActions();

        LoadJsonConversationToUI(dialogueFile, npcTalking, conversationIndex);
    }

    public void LoadJsonConversationToUI(TextAsset dialogueFile, NPC npcTalking, int conversationIndex = 0)
    {
        gameObject.SetActive(true);

        currentNPC = npcTalking;
        npcNameplate.color = currentNPC.primaryColor;
        foreach (DialogueOptionUI option in dialogueOptions) option.GetComponent<Image>().color = currentNPC.secondaryColor;

        loadedNPCDialogue = JsonConvert.DeserializeObject<NPCDialogue>(dialogueFile.text);
        npcPortrait.sprite = Resources.Load<Sprite>(PORTRAITFOLDER + loadedNPCDialogue.picture);
        npcName.text = loadedNPCDialogue.name;

        currentConversationIndex = conversationIndex;
        LoadCurrentConversationIndex(0);
    }

    public void LoadCurrentConversationIndex(int index)
    {
        NPCDialogue.Conversation conversation = loadedNPCDialogue.conversations[currentConversationIndex];
        NPCDialogue.DialogueItem dialogue = conversation.dialogues[index];
        npcDialoguePrompt.SetNewText(dialogue.prompt);

        for (int i = 0; i < dialogueOptions.Count; i++)
        {
            if (i < dialogue.responses.Length)
            {
                bool requirementsMet = true;
                var type = Type.GetType(loadedNPCDialogue.responseClass);
                DialogueResponse responseClass = (DialogueResponse)Activator.CreateInstance(type);

                if (dialogue.responses[i].requirement != null)
                {
                    var requirementMethod = type.GetMethod(dialogue.responses[i].requirement);
                    requirementsMet = (bool)requirementMethod.Invoke(responseClass, null);
                }

                dialogueOptions[i].gameObject.SetActive(requirementsMet);

                dialogueOptions[i].responseClass = responseClass;
                dialogueOptions[i].UpdateOption(dialogue.responses[i]);

            } else
            {
                dialogueOptions[i].gameObject.SetActive(false);
            }
        }

        optionsParent.SetActive(false);

        //ArrangeResponseOptions();
    }

    public void RevealDialogueOptions()
    {
        optionsParent.SetActive(true);
    }

    private void ArrangeResponseOptions()
    {
        int optionsVisible = 0;
        List<RectTransform> optionsToSet = new();

        foreach (DialogueOptionUI response in dialogueOptions)
        {
            if (response.gameObject.activeSelf)
            {
                optionsToSet.Add(response.GetComponent<RectTransform>());
                optionsVisible++;
            }
        }

        float spacePerElement = maxResponseUIWidth / optionsVisible;
        float actualElementWidth = spacePerElement / (1 + 2 * responseUIPadding);
        float actualPadding = actualElementWidth * responseUIPadding;

        float xPos = -maxResponseUIWidth / 2;

        foreach (RectTransform option in optionsToSet)
        {
            float xPosShift = actualElementWidth / 2 + actualPadding;
            if (optionsToSet.IndexOf(option) != 0) xPosShift *= 2;

            xPos += xPosShift;
            option.sizeDelta = new Vector2(actualElementWidth, option.sizeDelta.y);
            option.anchoredPosition = new Vector2(xPos, option.anchoredPosition.y);
        }
    }
}
