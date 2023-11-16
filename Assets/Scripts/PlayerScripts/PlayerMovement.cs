using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using static Cinemachine.CinemachineImpulseManager.ImpulseEvent;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(InputManager))]

public class PlayerMovement : MonoBehaviour
{
    [Header("Player movement stats:")]
    [SerializeField, Range(1, 100)] private float _moveSpeed;
    [SerializeField] private Vector2 _moveDirection;
    //
    [SerializeField] private float _buttonJumpTime;
    [SerializeField, Range(1, 100)] private float _jumpAmount;
    [SerializeField] private float _jumpTime;
    [SerializeField] private bool _jumping;
    //
    [SerializeField, Range(0, 10000)] private float _shiftAmount;
    [SerializeField] private bool _shiftAllowed;
    [SerializeField, Range(0, 30)] private float cooldownShift;
    //

    [Header("ACTIVE SKILLS:")]
    [SerializeField] private bool _activeJump;
    [SerializeField] private bool _activeShift;


    [Header("Player components:")]
    [SerializeField] private GroundCheckController _groundCheckController;
    private Rigidbody2D _rigidBody2D;

    private InputManager _inputManager;


    private void Awake()
    {
        _inputManager = GetComponent<InputManager>();
        _rigidBody2D= GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _shiftAllowed = true;
    }

    private void Start()
    {
        _inputManager.inputController.Player.Jump.started += context => OnJumpStart();
        _inputManager.inputController.Player.Jump.canceled += context => OnJumpCanceled();

        _inputManager.inputController.Player.Shift.performed += context => StartCoroutine(Shift());
    }

    private void Update()
    {
        _moveDirection = _inputManager.inputController.Player.Move.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        Move(_moveDirection);
        Flip();
        Jump();
    }

    //////////////////////////
    private void Move(Vector2 directionMove)
    {
        _rigidBody2D.velocity = new Vector2(directionMove.x * _moveSpeed, _rigidBody2D.velocity.y);
    }
    //////////////////////////

    //////////////////////////
    private void Flip()
    {
        if (_moveDirection.x < 0) transform.rotation = Quaternion.Euler(0, -180, 0);
        if (_moveDirection.x > 0) transform.rotation = Quaternion.Euler(0, 0, 0);
    }
    //////////////////////////

    //////////////////////////
    private void Jump()
    {
        if (_jumping)
        {
            _rigidBody2D.velocity = new Vector2(_rigidBody2D.velocity.x, _jumpAmount);
            _jumpTime += Time.deltaTime;

            if (_jumpTime > _buttonJumpTime)
            {
                _jumping = false;
            }
        }
    }

    private void OnJumpStart()
    {
        if (_groundCheckController.isGrounded && _activeJump)
        {
            Debug.Log("StartJump!");
            _jumping = true;
            _jumpTime = 0;
        }
    }

    private void OnJumpCanceled()
    {
        if (_activeJump)
        {
            Debug.Log("StopJump!");
            _jumping = false;
        }
    }
    //////////////////////////

    //////////////////////////

    private IEnumerator Shift()
    {
        if (_shiftAllowed && _activeShift)
        {
            _rigidBody2D.AddForce(_moveDirection * _shiftAmount, ForceMode2D.Force);
            Debug.Log("Shift!");
            _shiftAllowed = false;
            yield return new WaitForSeconds(cooldownShift);
            _shiftAllowed = true;
        }
    }
    //////////////////////////
}
