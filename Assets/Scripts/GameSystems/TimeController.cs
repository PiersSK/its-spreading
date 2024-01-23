using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeController : MonoBehaviour, IDataPersistence
{
    public static TimeController Instance { get; private set; }

    private const float REALDAYLENGTHMINS = 1440f;
    private const float REALDAYLENGTHSECONDS = 86400f;
    private const float REALHOURLENGTHSECONDS = 3600f;

    private const float REALMINUTELENGTHSECONDS = 60f;

    private const int SUNRISEHOURS = 8;
    private const int SUNSETHOURS = 19;
    private int daysComplete = 0;

    [Header("Time Settings")]
    [Range(0, 23)]
    [SerializeField] private int startTimeHours;
    [Range(1, 120)]
    [SerializeField] private int realtimeDayLengthMins = 60;
    [SerializeField] private TextMeshProUGUI clockUI;

    [Header("Lights")]
    [SerializeField] private GameObject sunPivot;
    [SerializeField] private Light sun;
    [SerializeField] private List<GameObject> roomLights;
    [SerializeField] private Color lowSunColor;

    [Header("Events")]
    public Transform scheduledEvents;
    //[SerializeField] private List<TimedEvent> scheduledEvents;
    public List<string> completeEvents;

    private float timeMultiplier;
    public float tempMultiplier = 1f;
    private float time = 0f;
    private float maxNaturalLight = 1.5f;

    private int currentHour;

    private int currentMinute;

    private bool isTimeSet = false;

    public void LoadData(GameData data)
    {
        completeEvents = data.completeEvents;
        currentHour = data.currentHour;
        currentMinute = data.currentMinute;
        daysComplete = data.daysComplete;
    }

    public void SaveData(ref GameData data)
    {
        data.completeEvents = completeEvents;
        data.currentHour = currentHour;
        data.currentMinute = currentMinute;
        data.daysComplete = daysComplete;
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if(!isTimeSet)
        {
            timeMultiplier = REALDAYLENGTHMINS / realtimeDayLengthMins;
            time = (currentHour * REALHOURLENGTHSECONDS) + (currentMinute * REALMINUTELENGTHSECONDS);
            isTimeSet = true;
        }

        // Update Time
        time += Time.deltaTime * timeMultiplier * tempMultiplier;
        if (time > REALDAYLENGTHSECONDS)
        {
            time = 0f;
            OnDayComplete();
        }

        currentHour = TimeSpanToHour(CurrentTime());
        currentMinute = TimeSpanToMinute(CurrentTime());
        UpdateClockUI();
        UpdateLights();
        TriggerEvents();
    }

    private void TriggerEvents()
    {
        foreach (Transform eventTransform in scheduledEvents)
        {
            if (eventTransform.TryGetComponent(out TimedEvent e))
            {
                if (e.ShouldEventTrigger())
                {
                    e.TriggerEvent();
                    e.hasBeenTriggered = true;
                }
            }

            if (e.TryGetComponent(out LimitedTimedEvent lte))
            {
                if (lte.ShouldEventEndTrigger())
                {
                    lte.TriggerEventEnd();
                    lte.eventHasEnded = true;
                }
            }
        }
    }

    private void UpdateLights()
    {
        float envLighting = 1f;
        bool isDaylight = time > SUNRISEHOURS * REALHOURLENGTHSECONDS && time < SUNSETHOURS * REALHOURLENGTHSECONDS;

        sunPivot.SetActive(isDaylight);
        foreach (GameObject light in roomLights) light.SetActive(!isDaylight);

        if (isDaylight)
        {
            float timePastSunrise = (time / REALHOURLENGTHSECONDS) - SUNRISEHOURS;
            float lengthOfDaylight = SUNSETHOURS - SUNRISEHOURS;

            sunPivot.transform.eulerAngles = new Vector3(180 * (timePastSunrise / lengthOfDaylight), 0, 0);

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
        clockUI.text = TimeSpanToClock(ts);
    }

    private Color GetSunColor(float percToLowSun)
    {
        Color sunColor = lowSunColor;
        sunColor.g = lowSunColor.g + (1 - lowSunColor.g) * percToLowSun;
        sunColor.b = lowSunColor.b + (1 - lowSunColor.b) * percToLowSun;
        return sunColor;
    }

    public bool TimeHasPassed(int hours, int minutes)
    {
        return time > hours * REALHOURLENGTHSECONDS + minutes * 60f;
    }
    
    private void OnDayComplete()
    {
        daysComplete++;
        completeEvents.Clear();
    }

    public TimeSpan CurrentTime()
    {
        return TimeSpan.FromSeconds(time);
    }

    public float InGameMinsToRealSeconds(int mins)
    {
        return (mins * 60f) / timeMultiplier;
    }

    public string TimeSpanToClock(TimeSpan ts)
    {
        string timeSuffix = ts.Hours < 12 ? "am" : "pm";
        int amPmHours = ts.Hours % 12 == 0 ? 12 : ts.Hours % 12;

        return string.Format("{0}:{1:00} {2}", amPmHours, ts.Minutes, timeSuffix);
    }

    public bool IsInTimeSpan(int hour1, int min1, int hour2, int min2)
    {
        return TimeHasPassed(hour1, min1) && !TimeHasPassed(hour2, min2);
    }

    public int TimeSpanToHour(TimeSpan ts)
    {
        int hour = ts.Hours;
        return hour;
    }

    public int TimeSpanToMinute(TimeSpan ts)
    {
        int minutes = ts.Minutes;
        return minutes;
    }
}
