using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    [SerializeField] private Transform confettiManPrefab;

    [SerializeField] private AudioSource genericSFXAudio;
    [SerializeField] private AudioSource bgMusic;
    [SerializeField] private AudioClip spreadingVoices;
    [SerializeField] private AudioClip partyBlower;
    [SerializeField] private AudioClip popSound;

    [SerializeField] private Image gameOverBackground;
    [SerializeField] private GameObject gameOverText;
    [SerializeField] private Button backToMenuButton;
    [SerializeField] private float gameoverFadeInTime;
    private float currentTimerGameOver = 0f;

    private Transform player;
    private Transform confettiMan;

    private const string MAINMENUSCENE = "MainMenu";


    private void Start()
    {
        player = Player.Instance.transform;
        TimeController.Instance.DayOver += DayIsOver;
        backToMenuButton.onClick.AddListener(BackToMainMenu);
    }

    private void Update()
    {
        if(currentTimerGameOver > 0)
        {
            currentTimerGameOver -= Time.deltaTime;
            gameOverBackground.color = new Color(0, 0, 0, 1 - (currentTimerGameOver / gameoverFadeInTime));
        } 
    }

    private void DayIsOver(object sender, System.EventArgs e)
    {
        TimeController.Instance.ToggleTimePause();
        Player.Instance.TogglePlayerIsEngaged();
        CameraController.Instance.SetCameraZoom(5f, 0.1f);
        bgMusic.Pause();
        genericSFXAudio.PlayOneShot(popSound);
        confettiMan = Instantiate(confettiManPrefab, player);
        confettiMan.localPosition = Vector3.zero;
        confettiMan.localEulerAngles = Vector3.zero;
        confettiMan.SetParent(null);
        confettiMan.localScale = Vector3.one;
        player.gameObject.SetActive(false);
        Invoke(nameof(ExplodeConfettiMan), 1.5f);
    }

    private void ExplodeConfettiMan()
    {
        genericSFXAudio.PlayOneShot(spreadingVoices);
        genericSFXAudio.PlayOneShot(partyBlower);
        foreach (Transform confetti in confettiMan)
        {
            Rigidbody rb = confetti.gameObject.AddComponent<Rigidbody>();
            rb.mass = 0.01f;
            rb.velocity = new Vector3(
                Random.Range(-10f, 10f),
                Random.Range(0, 10f),
                Random.Range(-10f, 10f)
            );
        }
        gameOverBackground.color = new Color(0, 0, 0, 0);
        gameOverBackground.gameObject.SetActive(true);
        currentTimerGameOver = gameoverFadeInTime;
        gameOverText.SetActive(true);
    }

    private void BackToMainMenu()
    {
        SceneManager.LoadScene(MAINMENUSCENE);
    }
}
