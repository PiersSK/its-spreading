using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleButton : MonoBehaviour
{
    [SerializeField] private Transform toggleObject;
    [SerializeField] private List<Transform> otherObjects;
    [SerializeField] private Transform frontParent;
    [SerializeField] private Transform backParent;
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(BringElementToFront);
    }

    private void BringElementToFront()
    {
        toggleObject.SetParent(frontParent);
        foreach (Transform t in otherObjects) t.SetParent(backParent);
    }
}
