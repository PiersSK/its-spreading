using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DanceSpreadingEvent : SpreadingEvent
{
    public float secondsOfDancingRequired;
    [SerializeField] private GameObject confidenceMeter;
    [SerializeField] private Image confidenceLevel;
    private float secondsOfDancing = 0f;
    private bool hasDancedSocksOff = false;

    protected override void Update()
    {
        base.Update();
        if(Player.Instance._animator.GetBool("isDancing") && !hasDancedSocksOff)
        {
            confidenceMeter.SetActive(true);
            secondsOfDancing += Time.deltaTime;
            confidenceLevel.fillAmount = secondsOfDancing / secondsOfDancingRequired;
            if (secondsOfDancing > secondsOfDancingRequired)
            {
                hasDancedSocksOff = true;
            }
        } else
        {
            confidenceMeter.SetActive(false);
        }
    }

    protected override bool ShouldEventTrigger()
    {
        return hasDancedSocksOff;
    }
}
