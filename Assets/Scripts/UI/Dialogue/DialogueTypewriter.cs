using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueTypewriter : MonoBehaviour
{
    private TextMeshProUGUI tmp;
    private IEnumerator activeCoroutine;
    [SerializeField] private float timeBetweenCharacters;
    [SerializeField] private float timeBetweenWords;

    private void Start()
    {
        tmp = GetComponent<TextMeshProUGUI>();
        if (tmp == null)
            Debug.LogError("DialogueTypewriter must be attached to an object with a TextMeshProUGUI component");
    }

    private void Update()
    {
        //test
        if(Input.GetMouseButtonDown(0))
        {
            if (tmp.maxVisibleCharacters < tmp.textInfo.characterCount)
            {
                tmp.maxVisibleCharacters = tmp.textInfo.characterCount;
                DialogueUI.Instance.RevealDialogueOptions();
                StopCoroutine(activeCoroutine);
                activeCoroutine = null;
            }
        }
    }

    public void SetNewText(string text)
    {
        tmp.text = text;
        activeCoroutine = TextVisible();
        StartCoroutine(activeCoroutine);
    }

    private IEnumerator TextVisible()
    {
        tmp.ForceMeshUpdate();

        int totalVisibleCharacters = tmp.textInfo.characterCount;
        int counter = 0;

        while (true)
        {
            int visibleCount = counter % (totalVisibleCharacters + 1);
            tmp.maxVisibleCharacters = visibleCount;

            if (visibleCount >= totalVisibleCharacters)
            {
                DialogueUI.Instance.RevealDialogueOptions();
                break;
            }

            counter++;
            yield return new WaitForSeconds(timeBetweenCharacters);
        }
    }
}
