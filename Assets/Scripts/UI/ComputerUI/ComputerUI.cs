using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComputerUI : MonoBehaviour
{
    public static ComputerUI Instance { get; private set; }

    public AudioSource computerAudio;
    [SerializeField] public AudioClip clickSound;
    public Image browserBackground;
    public List<GameObject> browserContent;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if(gameObject.activeSelf)
        {
            if(Input.GetMouseButtonDown(0))
            {
                computerAudio.PlayOneShot(clickSound);
            }
        }
    }

}
