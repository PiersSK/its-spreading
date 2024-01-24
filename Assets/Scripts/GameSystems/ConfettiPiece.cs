using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfettiPiece : MonoBehaviour
{
    public bool shouldFade = true;
    public float lifetime = 10f;
    private float lifeExpired = 0;
    public Room roomSettled;

    private void Update()
    {
        if(shouldFade)
        {
            lifeExpired += Time.deltaTime;
            if(lifeExpired > lifetime)
            {
                if(roomSettled != null) roomSettled.confettiInRoom--;
                Destroy(gameObject);
            }
        }
    }
}
