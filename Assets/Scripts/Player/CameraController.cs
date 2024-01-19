using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    private Vector3 initialPosition;
    
    void Start()
    {
        initialPosition = player.transform.position + new Vector3(-11f, 9.94f, -12);
        transform.rotation = Quaternion.Euler(new Vector3(30,45,0));
        transform.position = initialPosition;
    }

    void Update()
    {
        transform.position = player.transform.position + new Vector3(-11f, 9.94f, -12);
    }
}
