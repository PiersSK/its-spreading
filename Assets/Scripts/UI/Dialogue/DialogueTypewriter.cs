using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class DialogueTypewriter : MonoBehaviour
{
    private TextMeshProUGUI tmp;
    private IEnumerator activeCoroutine;

    Regex waitRegex = new Regex("<wait=([0-9])+>");
    private List<int> waitStartIndices = new();
    private List<int> waitValues = new();

    [SerializeField] private float timeBetweenCharacters;

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
        CheckForWaits();
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

            float waitTimer = timeBetweenCharacters;
            if (waitStartIndices.Contains(counter))
            {
                waitTimer += waitValues[waitStartIndices.IndexOf(counter)];
            }

            yield return new WaitForSeconds(waitTimer);
        }
    }

    private void CheckForWaits()
    {
        waitStartIndices = new();
        waitValues = new();

        int indexOffset = 0;

        foreach (Match match in waitRegex.Matches(tmp.text))
        {
            waitStartIndices.Add(match.Index - indexOffset + 1);
            waitValues.Add(Int32.Parse(match.Groups[1].ToString()));

            indexOffset += match.Value.Length;
        }

        tmp.text = Regex.Replace(tmp.text, @"<wait=[0-9]+>", "");
    }
}
