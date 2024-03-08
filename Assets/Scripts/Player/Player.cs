using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
public class Player : MonoBehaviour, IDataPersistence
{
    public static Player Instance { get; private set; }

    public event EventHandler<EventArgs> PlayerWaved;

    [SerializeField] private float speed = 5;
    [SerializeField] private float gravity = -9.8f;

    private CharacterController _characterController;
    public Animator _animator;

    private Vector3 playerVelocity;
    private Vector3 direction;

    public Controls playerControls;
    public Controls.PlayerActions controlActions;

    private PlayerInteract _playerInteract;
    public InventorySystem newInventory;

    public bool isUnengaged = true;
    private bool isMoving = false;
    private bool isDancing = false;

    [SerializeField] private float idleAnimTime = 10f;
    [SerializeField] private float idleCooldown = 5f;
    [SerializeField] private float idleTimer = 0f; // Serialized for debug
    private const int NUMBEROFIDLEANIMS = 4;

    public enum PlayerEmotes
    {
        Wave,
        Dance
    }

    private const string ANIMWALKING = "isWalking";
    private const string ANIMIDLE = "idle";
    private const string ANIMIDLEINDEX = "idleIndex";
    private const string ANIMWAVE = "wave";
    private const string ANIMDANCE = "isDancing";
    private const string ANIMINTERRUPT = "interruptIdle";

    public bool playerHasLearnedToDance = false;

    public Room currentRoom;

    public void LoadData(GameData data) { }

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
        newInventory = GetComponent<InventorySystem>();
        _animator = GetComponent<Animator>();

        //// Player Controls
        // Interactions
        controlActions.Interact.performed += ctx => Interact();
        controlActions.ToggleInteract.performed += ctx => ToggleInteract();

        // UI
        controlActions.Inventory.performed += ctx => InventoryUI.Instance.ToggleInventoryUI();
        controlActions.Phone.performed += ctx => PhoneUI.Instance.TogglePhone();
        controlActions.Pause.performed += ctx => EscapePressed();

        // Emotes
        controlActions.Emote1.performed += ctx => Emote(PlayerEmotes.Wave);
        controlActions.Emote2.performed += ctx => Emote(PlayerEmotes.Dance);

    }

    private void OnEnable()
    {
        controlActions.Enable();
    }

    private void OnDisable()
    {
        controlActions.Disable();
    }

    private void Update()
    {
        direction = ConvertToIsoVector(controlActions.Move.ReadValue<Vector3>());
        isMoving = direction != Vector3.zero;

        // Control basic animations
        if (isUnengaged) _animator.SetBool(ANIMWALKING, isMoving);
        if (isDancing && isMoving) ResetEmotes();

        if (!isMoving && !isDancing && isUnengaged)
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
        if (isUnengaged)
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

    // Interactions
    private void Interact()
    {
        if (!PauseMenu.isPaused)
        {
            _playerInteract.InteractWithSelected();
            idleTimer = 0f;
        }
    }

    private void ToggleInteract()
    {
        if (!PauseMenu.isPaused)
        {
            _playerInteract.CycleInteractable();
            idleTimer = 0f;
        }
    }

    private void Emote(PlayerEmotes emote)
    {
        if (!isUnengaged) return;

        switch(emote)
        {
            case PlayerEmotes.Wave:
                Wave();
                break;
            case PlayerEmotes.Dance:
                Dance();
                break;
        }
    }

    // Emotes
    private void Wave()
    {
        _animator.SetTrigger(ANIMWAVE);
        PlayerWaved?.Invoke(this, new EventArgs());
    }

    private void Dance()
    {
        if (playerHasLearnedToDance)
        {
            isDancing = !isDancing;
            _animator.SetBool(ANIMDANCE, isDancing);
            if (isDancing)
                CameraController.Instance.SetCameraZoom(5f, 10f);
            else
                CameraController.Instance.SetCameraZoom(9f, 0.2f);

            idleTimer = 0f;
        }
    }

    private void EscapePressed()
    {
        if (ComputerUI.Instance.gameObject.activeSelf)
        {
            ComputerUI.Instance.gameObject.SetActive(false);
        }
        else if (EndGame.Instance.creditsAreRolling)
        {
            EndGame.Instance.SkipCredits();
        }
        else
        {
            PauseMenu.TogglePause();
        }
    }

    public void LockPlayerIfNotEngaged(bool shouldDisableInteract = false, bool shouldDisableUI = false)
    {
        if (isUnengaged) TogglePlayerIsEngaged(shouldDisableInteract, shouldDisableUI);
    }

    public void FreePlayerIfEngaged(bool shouldDisableInteract = false, bool shouldDisableUI = false)
    {
        if (!isUnengaged) TogglePlayerIsEngaged(shouldDisableInteract, shouldDisableUI);
    }

    public void TogglePlayerIsEngaged(bool shouldDisableInteract = false, bool shouldDisableUI = false)
    {
        isUnengaged = !isUnengaged;
        _characterController.enabled = !_characterController.enabled;

        ToggleInputAction(controlActions.ToggleInteract);

        if (shouldDisableInteract) ToggleInputAction(controlActions.Interact);
        if (shouldDisableUI)
        {
            ToggleInputAction(controlActions.Phone);
            ToggleInputAction(controlActions.Inventory);
        }

        ResetEmotes();

        _animator.SetTrigger(ANIMINTERRUPT);
        _animator.SetBool(ANIMWALKING, false);

    }

    public void UnlockOptionalLockableActions()
    {
        if (!controlActions.Phone.enabled) ToggleInputAction(controlActions.Phone);
        if (!controlActions.Inventory.enabled) ToggleInputAction(controlActions.Inventory);
        if (!controlActions.Interact.enabled) ToggleInputAction(controlActions.Interact);
    }

    public void LockOptionalLockableActions()
    {
        if (controlActions.Phone.enabled) ToggleInputAction(controlActions.Phone);
        if (controlActions.Inventory.enabled) ToggleInputAction(controlActions.Inventory);
        if (controlActions.Interact.enabled) ToggleInputAction(controlActions.Interact);
    }

    private void ToggleInputAction(UnityEngine.InputSystem.InputAction action)
    {
        if (action.enabled)
            action.Disable();
        else
            action.Enable();
    }

    private void ResetEmotes()
    {
        isDancing = false;
        _animator.SetBool(ANIMDANCE, isDancing);
        CameraController.Instance.SetCameraZoom(9f, 0.2f);
    }

    public void ForcePlayerToPosition(Vector3 pos)
    {
        _characterController.enabled = false;
        transform.position = pos;
        _characterController.enabled = true;
    }
}