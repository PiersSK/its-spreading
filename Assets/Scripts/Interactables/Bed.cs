using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : Interactable
{
    private bool isInBed = false;

    [SerializeField] private Transform inBedPosition;
    [SerializeField] private float restTimeMultiplier;

    private Vector3 resetPosition;
    private Quaternion resetRotation;

    private Player player;

    private const string GETIN = "Rest";
    private const string GETOUT = "Get up";

    private void Start()
    {
        player = Player.Instance;
    }

    public override void Interact()
    {
        if(!isInBed)
        {

            player.GetComponent<PlayerInteract>().persistSelectedInteractable = true;
            player.TogglePlayerIsEngaged();

            resetPosition = player.transform.position;
            resetRotation = player.transform.rotation;

            player.transform.position = inBedPosition.position;
            player.transform.rotation = inBedPosition.rotation;

            TimeController.Instance.tempMultiplier = restTimeMultiplier;
            isInBed = true;
            promptText = GETOUT;
        }
        else
        {
            player.transform.position = resetPosition;
            player.transform.rotation = resetRotation;

            player.TogglePlayerIsEngaged();
            player.GetComponent<PlayerInteract>().persistSelectedInteractable = false;

            TimeController.Instance.tempMultiplier = 1f;
            isInBed = false;
            promptText = GETIN;
        }
    }
}
