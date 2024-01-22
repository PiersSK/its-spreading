using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance {  get; private set; }

    private GameObject currentTopUI;

    [SerializeField] private GameObject computerUI;
    [SerializeField] private GameObject pauseUI;
    [SerializeField] private GameObject phoneUI;

    private void Awake()
    {
        Instance = this;
    }

    private void DeactivateAllUI()
    {
        computerUI.SetActive(false);
        pauseUI.SetActive(false);
        phoneUI.SetActive(false);
    }

    public void ToggleComputerUI()
    {
        ToggleUIElement(computerUI);
    }

    public void TogglePauseUI()
    {
        ToggleUIElement(pauseUI);
    }

    public void TogglePhoneUI()
    {
        ToggleUIElement(phoneUI);
    }

    private void ToggleUIElement(GameObject uiElement)
    {
        if (currentTopUI != uiElement) currentTopUI = uiElement;
        else currentTopUI = null;

        bool shouldBeActive = !uiElement.activeSelf;
        DeactivateAllUI();
        uiElement.SetActive(shouldBeActive);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            phoneUI.SetActive(!phoneUI.activeSelf);
        }

    }


}
