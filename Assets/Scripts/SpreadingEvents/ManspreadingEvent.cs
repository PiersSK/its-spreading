using System.Collections;
using System.Collections.Generic;
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


    private void Start()
    {
        initialZoom = CameraController.Instance._camera.orthographicSize;
        realTimeTillManspread = TimeController.Instance.InGameMinsToRealSeconds(minsTillManspread);
        sofa.PlayerHasSat += Sofa_PlayerHasSat;
        sofa.PlayerHasStood += Sofa_PlayerHasStood;
    }

    private void Sofa_PlayerHasStood(object sender, System.EventArgs e)
    {
        ResetCamera();
    }

    private void Sofa_PlayerHasSat(object sender, System.EventArgs e)
    {
        if (!eventComplete)
        {
            CameraController.Instance.SetCameraZoom(manspreadZoom, realTimeTillManspread);
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

    protected override void Update()
    {
        base.Update(); // Needed to keep event check

        if (sofa.isLockedIn)
        {
            timeSpentSitting += Time.deltaTime;
            if (timeSpentSitting >= realTimeTillManspread)
            {
                Player.Instance.GetComponent<Animator>().SetTrigger("manspread");
                timeSpentSitting = 0f;
                Invoke(nameof(TrySetFirstManspread), manspreadAnimationLength);
            }
        }
        else
        {
            timeSpentSitting = 0f;
        }

    }

    protected override bool ShouldEventTrigger()
    {
        return firstManspreadTriggered;
    }
}
