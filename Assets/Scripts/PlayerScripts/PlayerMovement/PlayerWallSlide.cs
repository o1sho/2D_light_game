using UnityEngine;
using System.Collections;
using UnityEngine.SocialPlatforms;

[RequireComponent(typeof(PlayerMove))]
[RequireComponent(typeof(GroundCheckController))]
[RequireComponent(typeof(WallCheckController))]
public class PlayerWallSlide : MonoBehaviour
{
    [Header("PLAYER WALLSLIDE SETTINGS:")]
    private bool _isWallSliding;
    [SerializeField] private float _wallSlidingSpeed;
    public static bool canWallSlide = true;

    [Header("PLAYER WALLJUMP SETTINGS:")]
    //private bool _isWallJumping;
    private float _wallJumpingDirection;
    [SerializeField, Range(0, 1)] private float _wallJumpingTime;
    [SerializeField, Range(0, 1)] private float _wallJumpingDuration;
    [SerializeField] private Vector2 _wallJumpingPower;
    private Vector2 _moveDirection;

    [SerializeField] private int _amountStaminaToJump;

    [Header("PLAYER CLIMB LEDGE SETTINGS:")]
    private bool _canClimbLedge = false;
    //private Vector2 _ledgePosBot;
    [SerializeField] private Vector2 _ledgePos1;
    [SerializeField] private Vector2 _ledgePos2;
    [SerializeField] private float _ledgeClimbXOffset1 = 0f;
    [SerializeField] private float _ledgeClimbXOffset2 = 0f;
    [SerializeField] private float _ledgeClimbYOffset1 = 0f;
    [SerializeField] private float _ledgeClimbYOffset2 = 0f;

    //COMPONENTS:
    private InputManager _inputManager;
    private Rigidbody2D _rigidBody2D;
    private GroundCheckController _groundCheckController;
    private WallCheckController _wallCheckController;
    private Animator _animator;

    private PlayerStamina _playerStamina;

    private void Awake()
    {
        _inputManager= GetComponent<InputManager>();
        _rigidBody2D= GetComponent<Rigidbody2D>();
        _groundCheckController = GetComponent<GroundCheckController>();
        _wallCheckController = GetComponent<WallCheckController>();
        _animator= GetComponent<Animator>();

        _playerStamina= GetComponent<PlayerStamina>();
    }
    private void Start()
    {
        _inputManager.inputController.Player.Jump.performed += context => StartCoroutine(WallJump());
    }
    private void Update()
    {
        CheckInput();
        CheckIsWallSliding();
        if (canWallSlide) WallSlide();
        CheckLedgeClimb();
        UpdateStartLedgePosition();
    }
    private void FixedUpdate()
    {
        UpdateAnimations();
    }
    private void CheckInput()
    {
        _moveDirection = _inputManager.inputController.Player.Move.ReadValue<Vector2>();
    }
    private void CheckIsWallSliding()
    {
        if (_wallCheckController.isTouchingWall && !_groundCheckController.isGrounded && _rigidBody2D.velocity.y < 0.1 && !_canClimbLedge)
        {
            _isWallSliding= true;
            _playerStamina.StopHealStamina();
        }
        else
        {
            _isWallSliding= false;
            _playerStamina.StartHealStamina();
        }
    }

    private void WallSlide()
    {
        if (_isWallSliding)
        {
            _rigidBody2D.velocity = new Vector2(_rigidBody2D.velocity.x, Mathf.Clamp(_rigidBody2D.velocity.y, -_wallSlidingSpeed, float.MaxValue));
        }
    }
    private IEnumerator WallJump()
    {
        if (_isWallSliding && _wallCheckController.isTouchingWall && _playerStamina.GetStamina() >= _amountStaminaToJump)
        {
            //_isWallJumping = true;
            _wallJumpingDirection = transform.rotation.y == 0 ? -1 : 1;
            
            _playerStamina.SpendStamina(_amountStaminaToJump);

            _rigidBody2D.velocity = new Vector2(_wallJumpingDirection * _wallJumpingPower.x, _wallJumpingPower.y);
            yield return new WaitForSeconds(_wallJumpingDuration);
            //_isWallJumping = false;
        }
    }

    private void CheckLedgeClimb()
    {
        if (_wallCheckController.ledgeDetected && !_canClimbLedge)
        {
            _canClimbLedge = true;
            if (PlayerMove.isFacingRight)
            {
                _ledgePos1 = new Vector2(Mathf.Floor(_wallCheckController._ledgePosBot.x + _wallCheckController._checkDistance) - _ledgeClimbXOffset1, Mathf.Floor(_wallCheckController._ledgePosBot.y) + _ledgeClimbYOffset1);
                _ledgePos2 = new Vector2(Mathf.Floor(_wallCheckController._ledgePosBot.x + _wallCheckController._checkDistance) + _ledgeClimbXOffset2, Mathf.Floor(_wallCheckController._ledgePosBot.y) + _ledgeClimbYOffset2);
            }
            else
            {
                _ledgePos1 = new Vector2(Mathf.Ceil(_wallCheckController._ledgePosBot.x - _wallCheckController._checkDistance) + _ledgeClimbXOffset1, Mathf.Floor(_wallCheckController._ledgePosBot.y) + _ledgeClimbYOffset1);
                _ledgePos2 = new Vector2(Mathf.Ceil(_wallCheckController._ledgePosBot.x - _wallCheckController._checkDistance) - _ledgeClimbXOffset2, Mathf.Floor(_wallCheckController._ledgePosBot.y) + _ledgeClimbYOffset2);
            }
            PlayerMove.canMove = false;
            PlayerMove.canFlip = false;
            PlayerJump.canJump = false;
            //canWallSlide= false;
            _animator.SetBool("canClimbLedge", _canClimbLedge);
        }
        
    }
    private void UpdateStartLedgePosition()
    {
        if (_canClimbLedge)
        {
            transform.position = _ledgePos1;
        }
    }
    public void FinishLedgeClimb()
    {
        _canClimbLedge = false;
        transform.position = _ledgePos2;
        PlayerMove.canMove = true;
        PlayerMove.canFlip = true;
        PlayerJump.canJump = true;
        _wallCheckController.ledgeDetected = false;
        _animator.SetBool("canClimbLedge", _canClimbLedge);

    }

    private void UpdateAnimations()
    {
        _animator.SetBool("isWallSliding", _isWallSliding);
        //_animator.SetBool("canClimbLedge", _canClimbLedge);
        // _animator.SetBool("isWallJumping", _isWallSliding);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(_ledgePos1, _ledgePos2);
    }

}
