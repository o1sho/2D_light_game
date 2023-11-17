using System.Collections;
using Unity.VisualScripting;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using static Cinemachine.CinemachineImpulseManager.ImpulseEvent;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(InputManager))]

public class PlayerMovement : MonoBehaviour
{
    [Header("Player movement stats:")]
    [SerializeField, Range(1, 100)] private float _moveSpeed;
    private Vector2 _moveDirection;

    [Header("DASH SETTINGS:")]
    private bool _canDash = true;
    private bool _isDashing;
    [SerializeField] private float _dashingPower;
    [SerializeField, Range(0, 1)] private float _dashingTime;
    [SerializeField, Range(0, 20)] private float _dashingCooldown;

    [Header("JUMP SETTINGS:")]
    [SerializeField] private float _buttonJumpTime;
    [SerializeField, Range(1, 100)] private float _jumpPower;
    [SerializeField] private float _jumpTime;
    [SerializeField] private bool _jumping;

    [Header("WALL SLIDE & WALL JUMP SETTINGS:")]
    [SerializeField] private TouchCheckController _wallCheckController;
    private bool _isWallSliding;
    [SerializeField] private float _wallSlidingSpeed;
    private bool _isWallJumping;
    private float _wallJumpingDirection;
    [SerializeField, Range(0, 1)] private float _wallJumpingTime;
    [SerializeField, Range(0, 1)] private float _wallJumpingDuration;
    [SerializeField] private Vector2 _wallJumpingPower;


    [Header("ACTIVE SKILLS:")]
    [SerializeField] private bool _activeJump;
    [SerializeField] private bool _activeDash;
    [SerializeField] private bool _activeWallSlide;
    [SerializeField] private bool _activeWallJump;

    [Header("Player components:")]
    [SerializeField] private TouchCheckController _groundCheckController;
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

        _inputManager.inputController.Player.Jump.performed += context => StartCoroutine(WallJump());
    }

    private void Update()
    {
        _moveDirection = _inputManager.inputController.Player.Move.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        if (_isDashing) return;

        //Move(_moveDirection);
        //Flip();
        Jump();
        WallSlide();    
        WallJump();

        if (!_isWallJumping) Flip();
        if (!_isWallJumping) Move(_moveDirection);
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
        if (_groundCheckController.isTouched && _activeJump)
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
        if (_moveDirection.x != 0 && _activeDash && _canDash && !_isWallSliding)
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

    //////////////////////////
    private void WallSlide()
    {
        if (_wallCheckController.isTouched && !_groundCheckController.isTouched && _activeWallSlide)
        {
            _isWallSliding = true;
            _rigidBody2D.velocity = new Vector2(_rigidBody2D.velocity.x, Mathf.Clamp(_rigidBody2D.velocity.y, - _wallSlidingSpeed, float.MaxValue));
        }
        else
        {
            _isWallSliding = false;
        }
    }

    private IEnumerator WallJump()
    {
        if (_activeWallJump && _isWallSliding && _moveDirection.x != 0)
        {
            _isWallJumping = true;
            _wallJumpingDirection = -_moveDirection.x;
            if (_wallJumpingDirection > 0) transform.rotation = Quaternion.Euler(0, 0, 0);
            if (_wallJumpingDirection < 0) transform.rotation = Quaternion.Euler(0, -180, 0);

            _rigidBody2D.velocity = new Vector2(_wallJumpingDirection * _wallJumpingPower.x, _wallJumpingPower.y);
            yield return new WaitForSeconds(_wallJumpingDuration);
            _isWallJumping = false;
        }
    }
    //////////////////////////
}
