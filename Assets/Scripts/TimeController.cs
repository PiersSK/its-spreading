using System;
using TMPro;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    private const float REALDAYLENGTHMINS = 1440f;
    private const float REALDAYLENGTHSECONDS = 86400f;
    private const float REALHOURLENGTHSECONDS = 3600f;

    private const int SUNRISEHOURS = 8;
    private const int SUNSETHOURS = 19;


    [Range(0, 23)]
    [SerializeField] private int startTimeHours = 8;
    [Range(1, 120)]
    [SerializeField] private int realtimeDayLengthMins = 60;
    [SerializeField] private TextMeshProUGUI clockUI;

    private float timeMultiplier;
    private float time = 0f;
    private float maxNaturalLight = 2f;

    private void Start()
    {
        timeMultiplier = REALDAYLENGTHMINS / realtimeDayLengthMins;
        time = startTimeHours * REALHOURLENGTHSECONDS;
    }

    private void Update()
    {
        // Update Time
        time += Time.deltaTime * timeMultiplier;
        if (time > REALDAYLENGTHSECONDS) time = 0f;
        UpdateClockUI();

        // Update Lights
        float envLighting = 1f;
        if (time > SUNRISEHOURS * REALHOURLENGTHSECONDS && time < SUNSETHOURS * REALHOURLENGTHSECONDS)
        {
            float timePastSunrise = (time / REALHOURLENGTHSECONDS) - SUNRISEHOURS;
            float lengthOfDaylight = SUNSETHOURS - SUNRISEHOURS;

            if (timePastSunrise > lengthOfDaylight / 2)
                timePastSunrise = lengthOfDaylight - timePastSunrise;

            envLighting += maxNaturalLight * (timePastSunrise / (lengthOfDaylight / 2));
        }
        RenderSettings.ambientIntensity = envLighting;
    }

    private void UpdateClockUI()
    {
        TimeSpan ts = TimeSpan.FromSeconds(time);
        string timeSuffix = ts.Hours < 12 ? "am" : "pm";
        int amPmHours = ts.Hours % 12 == 0 ? 12 : ts.Hours % 12;

        clockUI.text = string.Format("{0}:{1:00} {2}", amPmHours, ts.Minutes, timeSuffix);
    }
}
