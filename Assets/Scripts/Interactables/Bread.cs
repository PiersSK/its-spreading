using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bread : Interactable, IDataPersistence
{
    public bool isSpreading = false;
    public bool isInteractable = true;

    private const string JAM = "Jam";
    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        promptText = JAM;
        _audioSource = GetComponent<AudioSource>();
        if (isSpreading)
        {
            transform.Rotate(0, 180, 0, Space.Self);
        }
    }

    // Update is called once per frame
    public override void Interact()
    {
        if (!isSpreading)
        {
            isSpreading = true;
            transform.Rotate(0, 180, 0, Space.Self);
            _audioSource.Play();
            isInteractable = false;
        }
    }

    public override bool CanInteract()
    {
        return !isSpreading && !TimeController.Instance.TimeHasPassed(10, 0) && isInteractable;
    }

    public void LoadData(GameData data)
    {
        isSpreading = data.breadIsFlipped;
    }

    public void SaveData(ref GameData data)
    {
        data.breadIsFlipped = isSpreading;
    }
}