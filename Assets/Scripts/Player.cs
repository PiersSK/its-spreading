using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 100;
    public Rigidbody _rb;
    private Vector3 _direction;
    public Controls _playerControls;
    private InputAction move;


    void Awake()
    {
        _playerControls = new Controls();
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        _direction = ConvertToIsoVector(move.ReadValue<Vector3>());
    }

    void FixedUpdate()
    {
        _rb.velocity = _direction * _speed * Time.deltaTime;
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
        move = _playerControls.Player.Move;
        move.Enable();
    }

    private void OnDisable()
    {
        move.Disable();   
    }
}
