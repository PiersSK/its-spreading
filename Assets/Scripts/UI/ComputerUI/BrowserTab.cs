using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrowserTab : MonoBehaviour
{
    [SerializeField] private GameObject page;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(SwitchToTab);
    }

    private void SwitchToTab()
    {
        foreach (GameObject tab in ComputerUI.Instance.browserContent) tab.SetActive(false);
        page.SetActive(true);

        ComputerUI.Instance.browserBackground.color = GetComponent<Image>().color;
    }
}
