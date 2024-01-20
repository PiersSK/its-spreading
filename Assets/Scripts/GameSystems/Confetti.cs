using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Confetti : MonoBehaviour
{
    public static Confetti Instance { get; private set; }

    [Header("Global Setttings")]
    [SerializeField] private int confettiGlobalLimit = 500;
    [SerializeField] private int confettiFadeChance = 80;
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 spawnOffset;

    [Header("Explosion Effect")]
    [SerializeField] private int confettiPerExplosion;
    [SerializeField] private float explosionVerticalVelocity;
    [SerializeField] private float explosionHorizontalVelocity;
    [SerializeField] private float explosionRadius;

    [Header("Resources")]
    [SerializeField] private List<Material> confettiMaterials;
    [SerializeField] private AudioClip partyBlowerSound;
    [SerializeField] private AudioClip yaySound;

    private AudioSource audioSource;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void ConfettiExplosion()
    {
        // Show title. TODO: Add our voice effect to this instead of "Yay"
        ConfettiUI.Instance.ShowItsSpreading();
        audioSource.PlayOneShot(yaySound);
        audioSource.PlayOneShot(partyBlowerSound);

        // Spawn confetti
        for (int i = 0; i < confettiPerExplosion; i++)
        {
            Transform confetti = Instantiate(Resources.Load<Transform>("ConfettiPiece"), transform);
            confetti.position = player.position + spawnOffset;

            // Randomly rotate and move to starting position
            confetti.localEulerAngles = new Vector3(Random.Range(0, 180), Random.Range(0, 180), Random.Range(0, 180));
            confetti.position += new Vector3(
                Random.Range(-explosionRadius, explosionRadius),
                Random.Range(0, explosionRadius),
                Random.Range(-explosionRadius, explosionRadius)
            );

            // Randomly set color
            confetti.GetComponent<MeshRenderer>().material = confettiMaterials[Random.Range(0, confettiMaterials.Count)];

            // Randomly set explosion velocity
            confetti.GetComponent<Rigidbody>().velocity = new Vector3(
                Random.Range(-explosionHorizontalVelocity, explosionHorizontalVelocity),
                explosionVerticalVelocity,
                Random.Range(-explosionHorizontalVelocity, explosionHorizontalVelocity)
            );

            // Randomly set if and for how long a piece should sit on the ground
            confetti.GetComponent<ConfettiPiece>().lifetime = confetti.GetComponent<ConfettiPiece>().lifetime * Random.Range(0.7f, 1.0f);
            confetti.GetComponent<ConfettiPiece>().shouldFade = Random.Range(0, 100) < confettiFadeChance;
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            ConfettiExplosion();
        }

        // Despawn oldest confetti if total amount is above the limit
        if (transform.childCount > confettiGlobalLimit)
        {
            for(int i = 0; i < transform.childCount - confettiGlobalLimit; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }

    }
}
