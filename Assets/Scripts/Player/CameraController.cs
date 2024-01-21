using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance { get; private set; }

    public Transform player;
    public Camera _camera;
    private Vector3 initialPosition;

    private float zoomInitial = 9f;
    private float zoomTarget = 9f;
    private float zoomTargetTime = 0f;
    private float zoomTimer = 0f;
    private bool isZoomingIn;

    private void Awake()
    {
        Instance = this;
        _camera = GetComponent<Camera>();
    }

    private void Start()
    {
        initialPosition = player.transform.position + new Vector3(-11f, 9.94f, -12);
        transform.rotation = Quaternion.Euler(new Vector3(30,45,0));
        transform.position = initialPosition;
    }

    private void Update()
    {
        transform.position = player.transform.position + new Vector3(-11f, 9.94f, -12);

        if((int)(_camera.orthographicSize*10) != (int)(zoomTarget*10))
        {
            if (isZoomingIn & _camera.orthographicSize < zoomTarget)
                _camera.orthographicSize = zoomTarget;
            else if (!isZoomingIn & _camera.orthographicSize > zoomTarget)
                _camera.orthographicSize = zoomTarget;
            else
            {
                zoomTimer += Time.deltaTime;
                _camera.orthographicSize = zoomInitial + (zoomTarget - zoomInitial) * (zoomTimer / zoomTargetTime);
            }
        }
    }

    public void SetCameraZoom(float zoom, float secondsToZoom)
    {
        if (zoomTarget == zoom) return;

        zoomTimer = 0f;
        zoomTarget = zoom;
        zoomTargetTime = secondsToZoom;
        zoomInitial = _camera.orthographicSize;

        isZoomingIn = zoomInitial > zoomTarget;
    }
}
