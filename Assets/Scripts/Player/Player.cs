using UnityEngine;
public class Player : MonoBehaviour, IDataPersistence
{
    public static Player Instance { get; private set; }

    [SerializeField] private float speed = 5;
    [SerializeField] private float gravity = -9.8f;

    private CharacterController _characterController;
    private Animator _animator;

    private Vector3 playerVelocity;
    private Vector3 direction;

    public Controls playerControls;
    private Controls.PlayerActions controlActions;

    private PlayerInteract _playerInteract;

    public bool canMove = true;
    private bool isMoving = false;

    [SerializeField] private float idleAnimTime = 10f;
    [SerializeField] private float idleCooldown = 5f;
    [SerializeField] private float idleTimer = 0f; // Serialized for debug
    private const int NUMBEROFIDLEANIMS = 4;

    private const string ANIMWALKING = "isWalking";
    private const string ANIMIDLE = "idle";
    private const string ANIMIDLEINDEX = "idleIndex";
    private const string ANIMWAVE = "wave";

    public void LoadData(GameData data)
    {
        _characterController.enabled = false;
        this.transform.position = data.playerPosition;
        _characterController.enabled = true;
    }

    public void SaveData(ref GameData data)
    {
        data.playerPosition = this.transform.position;
    }

    void Awake()
    {
        Instance = this;

        playerControls = new Controls();
        controlActions = playerControls.Player;

        _characterController = GetComponent<CharacterController>();
        _playerInteract = GetComponent<PlayerInteract>();
        _animator = GetComponent<Animator>();

        controlActions.Interact.performed += ctx => Interact();
        controlActions.ToggleInteract.performed += ctx => ToggleInteract();
        controlActions.Emote1.performed += ctx => Wave();
    }

    private void Update()
    {
        direction = ConvertToIsoVector(controlActions.Move.ReadValue<Vector3>());
        isMoving = direction != Vector3.zero;

        // Control basic animations
        if (canMove) _animator.SetBool(ANIMWALKING, isMoving);
        if (!isMoving)
        {
            idleTimer += Time.deltaTime;
            if(idleTimer >= idleAnimTime)
            {
                _animator.SetInteger(ANIMIDLEINDEX, Random.Range(0, NUMBEROFIDLEANIMS));
                _animator.SetTrigger(ANIMIDLE);
                idleTimer = -idleCooldown;
            }
        } else
        {
            idleTimer = 0f;
        }
    }

    void FixedUpdate()
    {
        if (canMove)
        {
            _characterController.Move(direction.normalized * speed * Time.deltaTime);

            playerVelocity.y += gravity * Time.deltaTime;
            if (_characterController.isGrounded && playerVelocity.y < 0)
                playerVelocity.y = -2.0f;
            _characterController.Move(playerVelocity * Time.deltaTime);

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

    private void Wave()
    {
        _animator.SetTrigger(ANIMWAVE);
        idleTimer = 0f;
    }

    private void Interact()
    {
        _playerInteract.InteractWithSelected();
        idleTimer = 0f;
    }

    private void ToggleInteract()
    {
        _playerInteract.CycleInteractable();
        idleTimer = 0f;
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
        _characterController.enabled = !_characterController.enabled;
        if (controlActions.ToggleInteract.enabled)
            controlActions.ToggleInteract.Disable();
        else
            controlActions.ToggleInteract.Enable();

    }
}