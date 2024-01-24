using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button newGame;
    [SerializeField] private Button settings;

    [SerializeField] private GameObject settingsObject;

    [SerializeField] private Slider curtainSlider;
    [SerializeField] private RectTransform curtainLeft;
    [SerializeField] private RectTransform curtainRight;
    private bool curtainPulled = false;

    [SerializeField] private AudioClip spreadingVoice;

    private Vector3 leftOrigin;
    private Vector3 rightOrigin;

    private void Start()
    {
        newGame.onClick.AddListener(StartGame);
        settings.onClick.AddListener(ToggleSettings);

        leftOrigin = curtainLeft.anchoredPosition;
        rightOrigin = curtainRight.anchoredPosition;
    }

    private void Update()
    {
        if (curtainSlider.value < curtainSlider.maxValue)
        {
            Vector3 left = leftOrigin;
            Vector3 right = rightOrigin;

            left.x *= Mathf.Clamp(Mathf.Exp(curtainSlider.value - 6) + 1, 1f, 10f);
            right.x *= Mathf.Clamp(Mathf.Exp(curtainSlider.value - 6) + 1, 1f, 10f);

            curtainLeft.anchoredPosition = left;
            curtainRight.anchoredPosition = right;
        } else if (!curtainPulled)
        {
            curtainSlider.gameObject.SetActive(false);
            Confetti.Instance.ConfettiExplosion(spreadingVoice, false);
            curtainPulled = true;
        }
    }

    private void StartGame()
    {
        SceneManager.LoadScene("Apartment");
    }

    private void ToggleSettings()
    {
        settingsObject.SetActive(!settingsObject.activeSelf);
    }
}
