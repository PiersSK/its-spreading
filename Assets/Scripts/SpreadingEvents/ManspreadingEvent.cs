using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ManspreadingEvent : SpreadingEvent
{
    [SerializeField] private Seat sofa;
    [SerializeField] private float manspreadAnimationLength;

    [Header("Trigger Settings")]
    [Range(10, 120)]
    [SerializeField] private int minsTillManspread;
    private float realTimeTillManspread;
    private float timeSpentSitting = 0f;
    private bool firstManspreadTriggered = false;

    [Header("Camera Zoom")]
    [SerializeField] private float manspreadZoom;
    [SerializeField] private float zoomOutTime;
    private float zoomInTime;
    private float initialZoom;

    [Header("Spreading Prompts")]
    [SerializeField] private GameObject promptUI;
    [SerializeField] private TextMeshProUGUI promptText;
    [SerializeField] private float timeBetweenPrompt = 7.5f;

    private bool waitingForPrompt = true;
    private float promptTimer = 0f;
    private int checkpoint = 0;
    private int maxCheckpoint = 2;

    private List<string> thoughts = new ()
    {
        "Maybe just this once..."
        ,"I mean, I'm all alone..."
        ,"Getting comfortable can't hurt, right?"
    };

    private List<KeyCode> promptKeys = new()
    {
        KeyCode.M,
        KeyCode.A,
        KeyCode.N
    };

    private void Start()
    {
        initialZoom = CameraController.Instance._camera.orthographicSize;
        realTimeTillManspread = TimeController.Instance.InGameMinsToRealSeconds(minsTillManspread);
        sofa.PlayerHasSat += Sofa_PlayerHasSat;
        sofa.PlayerHasStood += Sofa_PlayerHasStood;
    }
    protected override void Update()
    {
        base.Update(); // Needed to keep event check

        if (sofa.isLockedIn)
        {
            // First Time Manspread Loop
            if (checkpoint <= maxCheckpoint)
            {
                if (!waitingForPrompt)
                {
                    promptTimer -= Time.deltaTime;
                    if (promptTimer < 0)
                    {
                        waitingForPrompt = true;
                        ShowRelaxPrompt();
                    }
                }
                else if (Input.GetKeyDown(promptKeys[checkpoint]))
                {
                    IncrementCheckpoint();
                }
            }
            else 
            {
                // General Manspread Loop
                timeSpentSitting += Time.deltaTime;
                if (timeSpentSitting >= realTimeTillManspread)
                {
                    Player.Instance.GetComponent<Animator>().SetTrigger("manspread");
                    timeSpentSitting = 0f;
                    Invoke(nameof(TrySetFirstManspread), manspreadAnimationLength);
                }
            }
        }

    }

    private void IncrementCheckpoint()
    {
        ThoughtBubble.Instance.ShowThought(thoughts[checkpoint], null, timeBetweenPrompt);
        CameraController.Instance.SetCameraZoom(7f - checkpoint, 0.2f);
        promptTimer = timeBetweenPrompt;
        waitingForPrompt = false;
        checkpoint++;
        promptUI.SetActive(false);
    }
    private void ShowRelaxPrompt()
    {
        List<string> button = new() { "M", "A", "N" };
        promptText.text = button[checkpoint];
        promptUI.SetActive(true);
    }

    private void Sofa_PlayerHasStood(object sender, System.EventArgs e)
    {
        ResetCamera();
        promptUI.SetActive(false);
        waitingForPrompt = false;
    }

    private void Sofa_PlayerHasSat(object sender, System.EventArgs e)
    {
        if (!eventComplete)
        {
            checkpoint = 0;
            promptTimer = timeBetweenPrompt / 2f;
            timeSpentSitting = realTimeTillManspread;
            waitingForPrompt = false;
        }
    }

    private void ResetCamera()
    {
        CameraController.Instance.SetCameraZoom(initialZoom, zoomOutTime);
    }

    private void TrySetFirstManspread()
    {
        if (!eventComplete)
        {
            firstManspreadTriggered = true;
            ResetCamera();
        }
    }

    protected override bool ShouldEventTrigger()
    {
        return firstManspreadTriggered;
    }
}
