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
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        isPaused = true;
        pauseMenu.SetActive(true);
        //MusicController.Instance.PausePlayMusic(); keeping this as I think the below fix is a touch extreme, we may want _some_ form of music on the pause screen in future
        AudioListener.pause = true;
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        isPaused = false;
        pauseMenu.SetActive(false);
       // MusicController.Instance.PausePlayMusic(); see above.
        AudioListener.pause = false;
        Time.timeScale = 1f;
    }

    public void Quit()
    {
        Application.Quit();
    }

    

}