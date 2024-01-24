using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class LookAtMouse : MonoBehaviour
{
    [SerializeField] private Transform playerHead;
    [SerializeField] private Transform viewZLayer;

    [SerializeField] private float yOffset;
    private Camera mainCamera;
    void Start()
    {
        mainCamera = Camera.main;  // Don't keep calling Camera.main
    }

    void Update()
    {
        var lookAtPos = Input.mousePosition;

        lookAtPos.z = viewZLayer.position.z - playerHead.position.z;
        lookAtPos = mainCamera.ScreenToWorldPoint(lookAtPos);

        lookAtPos.x = playerHead.position.x + -(lookAtPos.x - playerHead.position.x);
        lookAtPos.y = playerHead.position.y + -(lookAtPos.y - playerHead.position.y) + yOffset;

        playerHead.LookAt(lookAtPos);
    }
}
