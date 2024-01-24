using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class NPCDialogue
{
    public string name;
    public string picture;
    public string responseClass;
    public Conversation[] conversations;

    [System.Serializable]

    public class Conversation
    {
        public int id;
        public DialogueItem[] dialogues;
    }
    [System.Serializable]

    public class DialogueItem : MonoBehaviour
    {
        public int id;
        public string prompt;
        public PlayerResponse[] responses;
    }
    [System.Serializable]

    public class PlayerResponse
    {
        public string text;
        public int? outcomeId;
        public string requirement;
        public string outcomeFunction;
    }
}
