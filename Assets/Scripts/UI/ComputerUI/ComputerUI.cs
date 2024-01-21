using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComputerUI : MonoBehaviour
{
    public static ComputerUI Instance { get; private set; }

    public Image browserBackground;
    public List<GameObject> browserContent;

    private void Awake()
    {
        Instance = this;
    }
}
