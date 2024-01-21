using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance {  get; private set; }

    private GameObject currentTopUI;

    [SerializeField] private GameObject computerUI;
    [SerializeField] private GameObject pauseUI;

    private void Awake()
    {
        Instance = this;
    }

    private void DeactivateAllUI()
    {
        computerUI.SetActive(false);
        pauseUI.SetActive(false);
    }

    public void ToggleComputerUI()
    {
        ToggleUIElement(computerUI);
    }

    public void TogglePauseUI()
    {
        ToggleUIElement(pauseUI);
    }

    private void ToggleUIElement(GameObject uiElement)
    {
        if (currentTopUI != uiElement) currentTopUI = uiElement;
        else currentTopUI = null;

        bool shouldBeActive = !uiElement.activeSelf;
        DeactivateAllUI();
        uiElement.SetActive(shouldBeActive);
    }
}
