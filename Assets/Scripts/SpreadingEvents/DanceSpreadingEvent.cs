using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DanceSpreadingEvent : SpreadingEvent
{
    public float secondsOfDancingRequired;
    [SerializeField] private GameObject confidenceMeter;
    [SerializeField] private Image confidenceLevel;
    [SerializeField] private TextMeshProUGUI motivationText;
    [SerializeField] private float danceEffort = 1f;
    [SerializeField] private float danceEffortDrainMultiplier = 2f;
    private float secondsOfDancing = 0f;
    private bool hasDancedSocksOff = false;

    private int checkpoint = 0;

    protected override void Update()
    {
        base.Update();
        if(Player.Instance._animator.GetBool("isDancing") && !hasDancedSocksOff)
        {
            if(Input.GetKeyDown(KeyCode.Alpha2))
            {
                secondsOfDancing += danceEffort;
            }
            confidenceMeter.SetActive(true);

            secondsOfDancing -= Time.deltaTime * danceEffortDrainMultiplier;
            secondsOfDancing = Mathf.Clamp(secondsOfDancing, 0f, secondsOfDancingRequired * 2f);

            confidenceLevel.fillAmount = secondsOfDancing / secondsOfDancingRequired;

            if (HasHitNextCheckpoint()) HitNextCheckpoint();

            if (secondsOfDancing > secondsOfDancingRequired)
            {
                hasDancedSocksOff = true;
            }
        } else
        {
            confidenceMeter.SetActive(false);
        }
    }

    private bool HasHitNextCheckpoint()
    {
        return secondsOfDancing >= secondsOfDancingRequired * (0.25f * (checkpoint + 1));
    }

    private void HitNextCheckpoint()
    {
        secondsOfDancing *= 0.5f;
        checkpoint++;

        switch(checkpoint)
        {
            case 1:
                motivationText.text = "You're doing so well, keep it up!";
                return;
            case 2:
                motivationText.text = "Looking good! The dancefloor won't know what hit it!";
                return;
            case 3:
                motivationText.text = "So almost there! Keep it up soldier!";
                return;
            case 4:
                motivationText.text = "RIGHT AT THE FINISH LINE! I'M SO PROUD OF YOU!!!!";
                return;
        }


    }

    protected override bool ShouldEventTrigger()
    {
        return hasDancedSocksOff;
    }

    protected override void EventImpact()
    {
        Player.Instance.playerHasLearedToDance = true;
    }
}
