using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bread : Interactable
{
    public bool isSpreading = false;

    private const string BUTTER = "Butter";

    // Start is called before the first frame update
    void Start()
    {
        promptText = BUTTER;
    }

    // Update is called once per frame
    public override void Interact()
    {
        isSpreading = true;
    }
}
