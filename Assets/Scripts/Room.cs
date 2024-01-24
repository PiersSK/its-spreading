using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public int confettiInRoom = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ConfettiPiece confetti))
        {
            confetti.roomSettled = this;
            confettiInRoom++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out ConfettiPiece confetti))
        {
            confetti.roomSettled = null;
            confettiInRoom--;
        }
    }
}
