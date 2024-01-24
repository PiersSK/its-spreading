using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ThoughtBubble : MonoBehaviour
{
    public static ThoughtBubble Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI message;
    [SerializeField] private float defaultTimer = 5f;
    [SerializeField] private AudioClip thoughtEnterExitSound;

    private float currentLife = 0f;

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
        if (currentLife > 0f)
        {
            currentLife -= Time.deltaTime;
            if (currentLife <= 0f)
            {
                currentLife = 0f;
                gameObject.SetActive(false);
                Player.Instance.GetComponent<AudioSource>().PlayOneShot(thoughtEnterExitSound);
            }
        } 
    }

    public void ShowThought(string thought)
    {
        message.text = thought;
        gameObject.SetActive(true);
        currentLife = defaultTimer;
        Player.Instance.GetComponent<AudioSource>().PlayOneShot(thoughtEnterExitSound);
    }
}
