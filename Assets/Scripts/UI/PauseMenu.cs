using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private TextMeshProUGUI volumeValueDisplay;

    public static bool isPaused = false;

    void Start()
    {
        pauseMenu.SetActive(false);
    }

    public static void TogglePause()
    {
        isPaused = !isPaused;
        UIManager.Instance.TogglePauseUI();
        AudioListener.pause = !AudioListener.pause;
        Time.timeScale = isPaused ? 0f : 1f;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void BackToMainMenu()
    {
        isPaused = false;
        Time.timeScale = 1f;
        AudioListener.pause = false;
        DataPersistenceManager.Instance.SaveGame();
        SceneManager.LoadScene("MainMenu");
    }

    public void RestartDay()
    {
        isPaused = false;
        Time.timeScale = 1f;
        AudioListener.pause = false;
        DataPersistenceManager.Instance.SaveNewDayToFile();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}