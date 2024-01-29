using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button newGame;
    [SerializeField] private Button resumeGame;
    [SerializeField] private Button settings;
    [SerializeField] private Button exit;

    private FileDataHandler fileHandler;
    private GameData gameData;

    [SerializeField] private GameObject settingsObject;

    [SerializeField] private Slider curtainSlider;
    [SerializeField] private RectTransform curtainLeft;
    [SerializeField] private RectTransform curtainRight;
    private bool curtainPulled = false;

    [SerializeField] private AudioClip spreadingVoice;

    private Vector3 leftOrigin;
    private Vector3 rightOrigin;

    private const string DEFAULTFILENAME = "its_spreading.game";
    private const string GAMESCENENAME = "Apartment";

    private void Start()
    {
        newGame.onClick.AddListener(StartGame);
        resumeGame.onClick.AddListener(ResumeGame);
        settings.onClick.AddListener(ToggleSettings);
        exit.onClick.AddListener(() => { Application.Quit(); });

        leftOrigin = curtainLeft.anchoredPosition;
        rightOrigin = curtainRight.anchoredPosition;

        fileHandler = new FileDataHandler(Application.persistentDataPath, DEFAULTFILENAME, true);
        gameData = fileHandler.Load();
        if(gameData == null || gameData.dayIsComplete) resumeGame.gameObject.SetActive(false);
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
            Confetti.Instance.ConfettiExplosion(spreadingVoice, string.Empty, false);
            curtainPulled = true;
        }
    }

    private void StartGame()
    {
        if (gameData != null)
        {
            gameData.dayIsComplete = true;
            fileHandler.Save(gameData);
        }
        SceneManager.LoadScene(GAMESCENENAME);
    }

    private void ResumeGame()
    {
        SceneManager.LoadScene(GAMESCENENAME);
    }

    private void ToggleSettings()
    {
        settingsObject.SetActive(!settingsObject.activeSelf);
    }
}
