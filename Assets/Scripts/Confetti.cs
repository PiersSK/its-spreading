using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Confetti : MonoBehaviour
{
    [SerializeField] private List<Material> confettiMaterials;
    [SerializeField] private int confettiPerExplosion;
    [SerializeField] private float explosionVelocity;
    [SerializeField] private float explosionRadius;

    [SerializeField] private AudioClip explosionSound;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        { // for party
            for(int i = 0; i < confettiPerExplosion; i++)
            {
                Transform confetti = Instantiate(Resources.Load<Transform>("ConfettiPiece"));
                confetti.parent = transform;
                confetti.localEulerAngles = new Vector3(Random.Range(0, 180), Random.Range(0, 180), Random.Range(0, 180));
                confetti.localPosition = new Vector3(
                    Random.Range(-explosionRadius, explosionRadius),
                    Random.Range(0, explosionRadius),
                    Random.Range(-explosionRadius, explosionRadius)
                );

                confetti.GetComponent<MeshRenderer>().material = confettiMaterials[Random.Range(0, confettiMaterials.Count)];
                confetti.GetComponent<Rigidbody>().velocity = new Vector3(
                    Random.Range(-explosionVelocity, explosionVelocity),
                    explosionVelocity * 2f,
                    Random.Range(-explosionVelocity, explosionVelocity)
                );
            }

            audioSource.PlayOneShot(explosionSound);
        }
    }
}
