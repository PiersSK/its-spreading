using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ThoughtBubble : MonoBehaviour
{
    public static ThoughtBubble Instance { get; private set; }
    public event EventHandler<ThoughBubbleDisplayedEventArgs> ThoughtBubbleDisplayed;
    public class ThoughBubbleDisplayedEventArgs
    {
        public string thoughtText;
    }

    [SerializeField] private TextMeshProUGUI message;
    [SerializeField] private float defaultTimer = 5f;
    [SerializeField] private AudioClip thoughtEnterExitSound;

    [SerializeField] private GameObject sourceIconObject;
    [SerializeField] private Image sourceIcon;

    public class Thought
    {
        public string message;
        public string prefabName;
        public float timer;
        public Thought(string message, string prefabName, float timer)
        {
            this.message = message;
            this.prefabName = prefabName;
            this.timer = timer;
        }
    }
    private List<Thought> messages = new ();

    private float currentLife = 0f;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void ShowThought(Thought thought)
    {
        messages.Add(thought);

    }

    public void UpdateThoughtQueue()
    {
        if (messages.Count > 0)
        {
            //  Show next message
            if (!gameObject.activeSelf)
            {
                currentLife = messages[0].timer;
                message.text = messages[0].message;
                gameObject.SetActive(true);
                if(messages[0].prefabName != null)
                {
                    sourceIconObject.SetActive(true);
                    sourceIcon.sprite = Resources.Load<Sprite>(messages[0].prefabName);
                }
                else
                {
                    sourceIconObject.SetActive(false);
                }
                Player.Instance.GetComponent<AudioSource>().PlayOneShot(thoughtEnterExitSound);
                ThoughtBubbleDisplayed?.Invoke(this, new ThoughBubbleDisplayedEventArgs() { thoughtText = messages[0].message });
            }

            // Decay current message life
            if (currentLife > 0f)
            {
                currentLife -= Time.deltaTime;
                if (currentLife <= 0f)
                {
                    currentLife = 0f;
                    gameObject.SetActive(false);
                    Player.Instance.GetComponent<AudioSource>().PlayOneShot(thoughtEnterExitSound);
                    messages.RemoveAt(0);
                }
            }
        }
    }
}
