using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueItem : MonoBehaviour
{
    public int id;
    public string prompt;
    public PlayerResponse[] responses;
}
