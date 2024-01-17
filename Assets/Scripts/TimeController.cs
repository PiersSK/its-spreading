using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeController : MonoBehaviour, IDataPersistence
{
    private const float REALDAYLENGTHMINS = 1440f;
    private const float REALDAYLENGTHSECONDS = 86400f;
    private const float REALHOURLENGTHSECONDS = 3600f;

    private const int SUNRISEHOURS = 8;
    private const int SUNSETHOURS = 19;
    private int daysComplete = 0;

    [Header("Time Settings")]
    [Range(0, 23)]
    [SerializeField] private int startTimeHours = 8;
    [Range(1, 120)]
    [SerializeField] private int realtimeDayLengthMins = 60;
    [SerializeField] private TextMeshProUGUI clockUI;

    [Header("Lights")]
    [SerializeField] private GameObject sunPivot;
    [SerializeField] private Light sun;
    [SerializeField] private List<GameObject> roomLights;
    [SerializeField] private Color lowSunColor;

    private float timeMultiplier;
    private float time = 0f;
    private float maxNaturalLight = 2f;

    public void LoadData(GameData data)
    {
        this.daysComplete = data.daysComplete;
    }

    public void SaveData(ref GameData data)
    {
        data.daysComplete = this.daysComplete;
    }
    
    private void Start()
    {
        timeMultiplier = REALDAYLENGTHMINS / realtimeDayLengthMins;
        time = startTimeHours * REALHOURLENGTHSECONDS;

    }

    private void Update()
    {
        // Update Time
        time += Time.deltaTime * timeMultiplier;
        if (time > REALDAYLENGTHSECONDS)
        {
            time = 0f;
            OnDayComplete();
        }
        UpdateClockUI();

        // Update Lights
        float envLighting = 1f;
        bool isDaylight = time > SUNRISEHOURS * REALHOURLENGTHSECONDS && time < SUNSETHOURS * REALHOURLENGTHSECONDS;

        sunPivot.SetActive(isDaylight);
        foreach (GameObject light in roomLights) light.SetActive(!isDaylight);

        if (isDaylight)
        {
            float timePastSunrise = (time / REALHOURLENGTHSECONDS) - SUNRISEHOURS;
            float lengthOfDaylight = SUNSETHOURS - SUNRISEHOURS;

            sunPivot.transform.eulerAngles = new Vector3(180 * (timePastSunrise / lengthOfDaylight), 0,0);

            if (timePastSunrise > lengthOfDaylight / 2)
                timePastSunrise = lengthOfDaylight - timePastSunrise;

            sun.color = GetSunColor(timePastSunrise / (lengthOfDaylight / 2));

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

    private Color GetSunColor(float percToLowSun)
    {
        Color sunColor = lowSunColor;
        sunColor.g = lowSunColor.g + (1 - lowSunColor.g) * percToLowSun;
        sunColor.b = lowSunColor.b + (1 - lowSunColor.b) * percToLowSun;
        return sunColor;
    }

    private void OnDayComplete()
    {
        daysComplete++;
    }
}
