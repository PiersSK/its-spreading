using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bathtub : Interactable
{
    private enum BathState
    {
        Filling,
        Draining,
        PartFilled,
        Filled,
        Drained,
        Occupied,
        Used
    }

    [SerializeField] private float secondsToFill = 5f;
    [SerializeField] private Transform waterLevel;
    private Vector3 waterAtEmpty;

    [SerializeField] private AudioClip waterFill;
    [SerializeField] private AudioClip waterDrain;
    [SerializeField] private AudioClip waterEnter;
    private AudioSource _audioSource;

    [SerializeField] private Transform inBathPosition;
    private Vector3 resetPosition;
    private Quaternion resetRotation;
    private Player player;

    private float currentFillLevel = 0f;
    private float maxFillHeight = 1f;

    private BathState currentState;
    private bool readyToDrain = false;

    private const string FILL = "Turn Tap On";
    private const string STOP = "Turn Tap Off";
    private const string GETIN = "Get In";
    private const string GETOUT = "Get Out";
    private const string DRAIN = "Drain Tub";

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        currentState = BathState.Drained;
        promptText = FILL;
        waterAtEmpty = waterLevel.localPosition;
        player = Player.Instance;
    }

    private void Update()
    {
        if (currentState == BathState.Filling) {
            if (currentFillLevel <= secondsToFill)
                currentFillLevel += Time.deltaTime;
            else
            {
                currentState = BathState.Filled;
                _audioSource.Pause();
                promptText = GETIN;
            }
        } else if (currentState == BathState.Draining)
        {
            if (currentFillLevel > 0f)
                currentFillLevel -= Time.deltaTime;
            else
            {
                currentState = BathState.Drained;
                promptText = FILL;
            }
        }

        waterLevel.localPosition = waterAtEmpty + new Vector3(0, maxFillHeight * (currentFillLevel/secondsToFill), 0);
    }

    public override void Interact()
    {
        switch (currentState)
        {
            case BathState.Drained:
                _audioSource.clip = waterFill;
                readyToDrain = false;
                currentState = BathState.Filling;
                _audioSource.Play();
                promptText = STOP;
                return;
            case BathState.Filling:
                currentState = BathState.PartFilled;
                _audioSource.Pause();
                promptText = FILL;
                return;
            case BathState.PartFilled:
                if (!readyToDrain)
                {
                    currentState = BathState.Filling;
                    _audioSource.Play();
                }
                else
                    currentState = BathState.Draining;

                promptText = STOP;
                return;
            case BathState.Filled:
                currentState = BathState.Occupied;
                _audioSource.clip = null;
                _audioSource.PlayOneShot(waterEnter, 1f);

                player.GetComponent<PlayerInteract>().persistSelectedInteractable = true;
                player.LockPlayerIfNotEngaged();

                resetPosition = player.transform.position;
                resetRotation = player.transform.rotation;

                player.transform.position = inBathPosition.position;
                player.transform.rotation = inBathPosition.rotation;
                promptText = GETOUT;
                return;
            case BathState.Occupied:
                currentState = BathState.Used;
                readyToDrain = true;

                player.transform.position = resetPosition;
                player.transform.rotation = resetRotation;

                player.FreePlayerIfEngaged();
                player.GetComponent<PlayerInteract>().persistSelectedInteractable = false;

                promptText = DRAIN;
                return;
            case BathState.Used:
                currentState = BathState.Draining;
                _audioSource.PlayOneShot(waterDrain, 1f);
                promptText = STOP;
                return;
            case BathState.Draining:
                currentState = BathState.PartFilled;
                promptText = DRAIN;
                return;
        }
    }
}
