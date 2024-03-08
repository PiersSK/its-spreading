using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    public static EndGame Instance { get; private set; }

    [SerializeField] private Transform confettiManPrefab;

    [SerializeField] private AudioSource genericSFXAudio;
    [SerializeField] private AudioSource genericMusicAudio;
    [SerializeField] private AudioSource bgMusic;
    [SerializeField] private AudioSource boombox;

    [SerializeField] private AudioClip spreadingVoices;
    [SerializeField] private AudioClip partyBlower;
    [SerializeField] private AudioClip popSound;
    [SerializeField] private AudioClip danceMusic;

    [SerializeField] private GameObject danceCamera;
    [SerializeField] private GameObject danceLights;
    [SerializeField] private GameObject creditsUI;
    [SerializeField] private GameObject gameUI;

    [SerializeField] private GameObject successScreen;
    [SerializeField] private Button successbackToMenuButton;
    [SerializeField] private Button exitToDesktopButton;
    [SerializeField] private Button gameJamButton;


    [SerializeField] private Image gameOverBackground;
    [SerializeField] private GameObject gameOverText;
    [SerializeField] private Button backToMenuButton;
    [SerializeField] private float gameoverFadeInTime;
    private float currentTimerGameOver = 0f;

    public bool creditsAreRolling = false;

    private Transform player;
    private Transform confettiMan;

    private const string MAINMENUSCENE = "MainMenu";
    private const string GAMEJAMITCHIO = "https://itch.io/jam/pirate/entries";

    [SerializeField] private List<Transform> npcs;
    [SerializeField] private List<Transform> npcLocations;
    [SerializeField] private Transform playerDanceLocation;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        player = Player.Instance.transform;
        TimeController.Instance.DayOver += DayIsOver;
        backToMenuButton.onClick.AddListener(BackToMainMenu);
        successbackToMenuButton.onClick.AddListener(BackToMainMenu);
        exitToDesktopButton.onClick.AddListener(() => { Application.Quit(); });
        gameJamButton.onClick.AddListener(() => { Application.OpenURL(GAMEJAMITCHIO); });
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
        DayIsOver();
    }

    public void DayIsOver()
    {
        TimeController.Instance.ToggleTimePause();
        Player.Instance.LockPlayerIfNotEngaged();

        if (ObjectiveController.HasCompletedAllObjectives())
        {
            bgMusic.Pause();
            boombox.Pause();
            Player.Instance._animator.SetTrigger("success");
            Invoke(nameof(SuccessEnding), 6.2f);
        }
        else
            FailEnding();
    }

    public void SkipCredits()
    {
        creditsAreRolling = false;
        CancelInvoke();
        ShowSuccessSplash();
    }

    private void SuccessEnding()
    {
        Player.Instance.transform.SetParent(playerDanceLocation);
        Player.Instance.transform.localPosition = Vector3.zero;
        Player.Instance.transform.localEulerAngles = Vector3.zero;

        foreach(Transform npc in npcs)
        {
            npc.GetComponent<NavMeshAgent>().enabled = false;
            npc.SetParent(npcLocations[npcs.IndexOf(npc)]);
            npc.localPosition = Vector3.zero;
            npc.localEulerAngles = Vector3.zero;
            npc.GetComponent<Animator>().SetBool("isWalking", false);
        }

        Player.Instance._animator.SetBool("isDancing", true);
        foreach (Transform npc in npcs) npc.GetComponent<Animator>().SetBool("isDancing", true);

        genericMusicAudio.clip = danceMusic;
        genericMusicAudio.Play();

        TimeController.Instance.TurnOffAllLights();

        Camera.main.gameObject.SetActive(false);
        gameUI.SetActive(false);
        danceCamera.SetActive(true);
        danceLights.SetActive(true);
        creditsUI.SetActive(true);

        Invoke(nameof(AllWave), 90f);
        Invoke(nameof(ShowSuccessSplash), 94f);

        creditsAreRolling = true;
    }

    private void AllWave()
    {
        Player.Instance._animator.SetBool("isDancing", false);
        foreach (Transform npc in npcs) npc.GetComponent<Animator>().SetBool("isDancing", false);

        Player.Instance._animator.SetTrigger("wave");
        foreach (Transform npc in npcs) npc.GetComponent<Animator>().SetTrigger("wave");
    }

    private void ShowSuccessSplash()
    {
        successScreen.SetActive(true);
    }

    private void FailEnding()
    {
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
        DataPersistenceManager.Instance.SaveNewDayToFile();
        SceneManager.LoadScene(MAINMENUSCENE);
    }
}
