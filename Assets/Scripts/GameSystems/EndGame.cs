using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    [SerializeField] private Transform confettiManPrefab;
    private Transform player;


    private void Start()
    {
        player = Player.Instance.transform;
        TimeController.Instance.DayOver += DayIsOver;
    }

    private void DayIsOver(object sender, System.EventArgs e)
    {
        TimeController.Instance.ToggleTimePause();
        Player.Instance.TogglePlayerIsEngaged();
        CameraController.Instance.SetCameraZoom(5f, 0.1f);
        Transform confetti = Instantiate(confettiManPrefab, player);
        confetti.localPosition = Vector3.zero;
        confetti.localEulerAngles = Vector3.zero;
        confetti.SetParent(null);
        confetti.localScale = Vector3.one;
        player.gameObject.SetActive(false);
    }
}
