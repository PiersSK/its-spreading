using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class TvBroadcast : LimitedTimedEvent
{
    [SerializeField] public Light tvLight;
    [SerializeField] public Color originalColour;
    [SerializeField] public Color newColour;
    public override void TriggerEvent()
    {
        tvLight.color = newColour;
    }

    public override void TriggerEventEnd()
    {
        tvLight.color = originalColour;
    }
}