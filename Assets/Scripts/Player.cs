using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Player : MonoBehaviour
{
    [SerializeField]
    private float speed = 100;
    public Rigidbody _rb;
    private Vector3 direction;
    public Controls playerControls;
    private InputAction move;


    void Awake()
    {
        playerControls = new Controls();
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        direction = ConvertToIsoVector(move.ReadValue<Vector3>());
    }

    void FixedUpdate()
    {
        _rb.velocity = direction.normalized * speed * Time.deltaTime;

        if (direction != Vector3.zero)
        {
            _rb.rotation = transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 0.15F);
        }
    }

    private Vector3 ConvertToIsoVector(Vector2 inputVector)
    {
        Vector3 toConvert = new Vector3(inputVector.x, 0, inputVector.y);
        Quaternion rotation = Quaternion.Euler(0, 45.0f, 0);
        Matrix4x4 isoMatrix = Matrix4x4.Rotate(rotation);
        return isoMatrix.MultiplyPoint3x4(toConvert);
    }

    private void OnEnable()
    {
        move = playerControls.Player.Move;
        move.Enable();
    }

    private void OnDisable()
    {
        move.Disable();
    }
}