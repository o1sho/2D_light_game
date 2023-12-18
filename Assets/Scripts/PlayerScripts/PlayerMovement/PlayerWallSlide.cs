using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

[RequireComponent(typeof(PlayerMove))]
[RequireComponent(typeof(GroundCheckController))]
[RequireComponent(typeof(WallCheckController))]
public class PlayerWallSlide : MonoBehaviour
{
    [Header("PLAYER WALLSLIDE SETTINGS:")]
    private bool _isWallSliding;
    [SerializeField] private float _wallSlidingSpeed;

    [Header("PLAYER WALLJUMP SETTINGS:")]
    //private bool _isWallJumping;
    private float _wallJumpingDirection;
    [SerializeField, Range(0, 1)] private float _wallJumpingTime;
    [SerializeField, Range(0, 1)] private float _wallJumpingDuration;
    [SerializeField] private Vector2 _wallJumpingPower;
    private Vector2 _moveDirection;

    [SerializeField] private int _amountStaminaToJump;

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
    }
    private void FixedUpdate()
    {
        WallSlide();
        UpdateAnimations();
    }
    private void CheckInput()
    {
        _moveDirection = _inputManager.inputController.Player.Move.ReadValue<Vector2>();
    }
    private void CheckIsWallSliding()
    {
        if (_wallCheckController.isTouchingWall && !_groundCheckController.isGrounded && _rigidBody2D.velocity.y < 0.1)
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

    private void UpdateAnimations()
    {
        _animator.SetBool("isWallSliding", _isWallSliding);
       // _animator.SetBool("isWallJumping", _isWallSliding);
    }
}
