using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(InputManager))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Player movement stats:")]
    [SerializeField] private float _moveSpeed;
    //
    [SerializeField] private float _buttonJumpTime;
    [SerializeField] private float _jumpAmount;
    [SerializeField] private float _jumpTime;
    [SerializeField] private bool _jumping;
    //
    [SerializeField] private float _buttonShiftTime;
    [SerializeField] private float _shiftAmount;
    [SerializeField] private float _shiftTime;
    [SerializeField] private bool _shifting;
    //



    [Header("Gravity handling:")]


    [Header("Player components:")]
    private PlayerController _playerController;
    private InputManager _inputManager;
    private Rigidbody2D _rigidBody2D;

    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
        _inputManager = GetComponent<InputManager>();

        _rigidBody2D= GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {

    }

    private void Start()
    {
        _inputManager.inputController.Player.Jump.started += context => OnJumpStart();
        _inputManager.inputController.Player.Jump.canceled += context => OnJumpCanceled();

        //_inputManager.inputController.Player.Shift.performed += context => OnShift();
        _inputManager.inputController.Player.Shift.started += context => OnShiftStart();
        _inputManager.inputController.Player.Shift.canceled += context => OnShiftCanceled();
    }

    private void Update()
    {
    }

    private void FixedUpdate()
    {
        Vector2 moveDirection = _inputManager.inputController.Player.Move.ReadValue<Vector2>();
        Move(moveDirection);

        if (_jumping)
        {
            _rigidBody2D.velocity = new Vector2(_rigidBody2D.velocity.x, _jumpAmount);
            _jumpTime += Time.deltaTime;

            if (_jumpTime > _buttonJumpTime)
            {
                _jumping = false;
            }
        }

        if (_shifting)
        {
            _rigidBody2D.velocity = new Vector2(_shiftAmount, _rigidBody2D.velocity.y);
            _shiftTime += Time.deltaTime;

            if (_shiftTime > _buttonShiftTime)
            {
                _shifting = false;
            }
        }
    }

    private void Move(Vector2 directionMove)
    {
        Vector3 direction = new Vector3(directionMove.x, 0, 0);
        transform.position += direction * _moveSpeed * Time.deltaTime;
    }

    private void OnJumpStart()
    {
        if (_playerController.isGrounded)
        {
            Debug.Log("StartJump!");
            _jumping = true;
            _jumpTime = 0;
        }
    }

    private void OnJumpCanceled()
    {
        Debug.Log("StopJump!");
        _jumping = false;
    }

    private void OnShiftStart()
    {
        if (_playerController.isGrounded)
        {
            Debug.Log("StartShift!");
            _shifting = true;
            _shiftTime = 0;
        }
    }

    private void OnShiftCanceled()
    {
        Debug.Log("StopShift!");
        _shifting = false;
    }
}
