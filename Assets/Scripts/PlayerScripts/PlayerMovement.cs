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
    private Vector2 _moveDirection;

    [Header("JUMP SETTINGS:")]
    [SerializeField] private float _buttonJumpTime;
    [SerializeField, Range(1, 100)] private float _jumpPower;
    [SerializeField] private float _jumpTime;
    [SerializeField] private bool _jumping;

    [Header("DASH SETTINGS:")]
    private bool _canDash = true;
    private bool _isDashing;
    [SerializeField] private float _dashingPower;
    [SerializeField, Range(0, 1)] private float _dashingTime;
    [SerializeField, Range(0, 20)] private float _dashingCooldown;

    [Header("ACTIVE SKILLS:")]
    [SerializeField] private bool _activeJump;
    [SerializeField] private bool _activeShift;

    [Header("Player components:")]
    [SerializeField] private GroundCheckController _groundCheckController;
    private Rigidbody2D _rigidBody2D;
    [SerializeField] private TrailRenderer _trailRenderer;

    private InputManager _inputManager;


    private void Awake()
    {
        _inputManager = GetComponent<InputManager>();
        _rigidBody2D= GetComponent<Rigidbody2D>();
        _trailRenderer = GetComponent<TrailRenderer>();
    }

    private void OnEnable()
    {
    }

    private void Start()
    {
        _inputManager.inputController.Player.Jump.started += context => OnJumpStart();
        _inputManager.inputController.Player.Jump.canceled += context => OnJumpCanceled();

        _inputManager.inputController.Player.Dash.performed += context => StartCoroutine(Dash());
    }

    private void Update()
    {
        _moveDirection = _inputManager.inputController.Player.Move.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        if (_isDashing) return;

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
            _rigidBody2D.velocity = new Vector2(_rigidBody2D.velocity.x, _jumpPower);
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

    private IEnumerator Dash()
    {
        if (_moveDirection.x != 0 && _activeShift && _canDash)
        {
            _canDash = false;
            _isDashing = true;
            float originalGravity = _rigidBody2D.gravityScale;
            _rigidBody2D.gravityScale = 0f;
            //_rigidBody2D.AddForce(_moveDirection * _dashingPower, ForceMode2D.Force);
            _rigidBody2D.velocity = new Vector2(_moveDirection.x * _dashingPower, 0f);
            _trailRenderer.emitting = true;
            yield return new WaitForSeconds(_dashingTime);
            _trailRenderer.emitting = false;
            _rigidBody2D.gravityScale = originalGravity;
            _isDashing = false;
            yield return new WaitForSeconds(_dashingCooldown);
            _canDash = true;
        }
    }
    //////////////////////////
}
