using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneUI : MonoBehaviour
{
    public GameObject phoneUI;
    public static bool isActive = false;

    // Start is called before the first frame update
    void Start()
    {
        phoneUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.M)){OpenPhone();}
            
        if (isActive && Input.GetKeyDown(KeyCode.Escape))
        {
            PutAway();
        }
    }

    public void PutAway()
    {
        throw new NotImplementedException();
    }

    public void OpenPhone()
    {

    }



}
