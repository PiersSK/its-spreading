using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    [SerializeField] private float speed = 5;
    [SerializeField] private CharacterController _characterController;
    private Vector3 direction;

    public Controls playerControls;
    private Controls.PlayerActions controlActions;

    private PlayerInteract _playerInteract;

    public bool canMove = true;

    void Awake()
    {
        Instance = this;

        playerControls = new Controls();
        controlActions = playerControls.Player;

        _characterController = GetComponent<CharacterController>();
        _playerInteract = GetComponent<PlayerInteract>();

        controlActions.Interact.performed += ctx => _playerInteract.InteractWithSelected();
        controlActions.ToggleInteract.performed += ctx => _playerInteract.CycleInteractable();

    }

    private void Update()
    {
        direction = ConvertToIsoVector(controlActions.Move.ReadValue<Vector3>());
    }

    void FixedUpdate()
    {
        if (canMove)
        {
            _characterController.Move(direction.normalized * speed * Time.deltaTime);

            if (direction != Vector3.zero)
            {
                transform.rotation = transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 0.15F);
            }
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
        controlActions.Enable();
    }

    private void OnDisable()
    {
        controlActions.Disable();
    }

    public void TogglePlayerIsEngaged()
    {
        canMove = !canMove;
        if (controlActions.ToggleInteract.enabled)
            controlActions.ToggleInteract.Disable();
        else
            controlActions.ToggleInteract.Enable();
    }
}