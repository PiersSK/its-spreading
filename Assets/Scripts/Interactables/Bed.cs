using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : Interactable
{
    private bool isInBed = false;

    [SerializeField] private Transform inBedPosition;
    private Transform resetPosition;

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
            player.TogglePlayerIsEngaged();
            resetPosition = player.transform;

            player.transform.position = inBedPosition.position;
            player.transform.rotation = inBedPosition.rotation;

            isInBed = true;
            promptText = GETOUT;
        }
        else
        {
            player.TogglePlayerIsEngaged();
            player.transform.position = resetPosition.position;
            player.transform.rotation = resetPosition.rotation;
            isInBed = false;
            promptText = GETIN;
        }
    }
}
