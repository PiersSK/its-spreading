using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfettiExplosion : MonoBehaviour
{
    private float timeToExplode = 2f;
    private bool exploded = false;
    // Update is called once per frame
    void Update()
    {
        if (!exploded)
        {
            timeToExplode -= Time.deltaTime;
            if (timeToExplode < 0)
            {
                Rigidbody rb = gameObject.AddComponent<Rigidbody>();
                rb.velocity = new Vector3(
                    Random.Range(-10f, 10f),
                    Random.Range(0, 10f),
                    Random.Range(-10f, 10f)
                );
                exploded = true;
            }
        }
    }
}
