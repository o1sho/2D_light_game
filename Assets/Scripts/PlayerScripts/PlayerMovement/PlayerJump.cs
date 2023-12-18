using UnityEngine;

[RequireComponent(typeof(PlayerMove))]
[RequireComponent(typeof(GroundCheckController))]
public class PlayerJump : MonoBehaviour
{
    [Header("PLAYER JUMP SETTINGS:")]
    [SerializeField] private float _buttonJumpTime;
    [SerializeField, Range(1, 30)] private float _jumpPower;
    private float _jumpTime;
    private bool _jumping;

    [SerializeField] private int _amountStamina;

    //COMPONENTS:
    private InputManager _inputManager;
    private Rigidbody2D _rigidBody2D;
    private GroundCheckController _groundCheckController;
    private Animator _animator;

    private PlayerStamina _playerStamina;

    private void Awake()
    {
        _inputManager= GetComponent<InputManager>();
        _rigidBody2D= GetComponent<Rigidbody2D>();
        _groundCheckController= GetComponent<GroundCheckController>();
        _animator= GetComponent<Animator>();

        _playerStamina= GetComponent<PlayerStamina>();
    }
    private void Start()
    {
        _inputManager.inputController.Player.Jump.started += context => OnJumpStart();
        _inputManager.inputController.Player.Jump.canceled += context => OnJumpCanceled();
    }
    private void Update()
    {
    }
    private void FixedUpdate()
    {
        Jump();
        UpdateAnimations();
    }

    private void OnJumpStart()
    {
        if (_groundCheckController.isGrounded && _playerStamina.GetStamina() >= _amountStamina)
        {
            Debug.Log("StartJump!");
            _jumping = true;
            _jumpTime = 0;
            _playerStamina.SpendStamina(_amountStamina);
        }
    }
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
    private void OnJumpCanceled()
    {

        Debug.Log("StopJump!");
        _jumping = false;

    }
    private void UpdateAnimations()
    {
        _animator.SetFloat("velY", _rigidBody2D.velocity.y);
        _animator.SetBool("isGrounded", _groundCheckController.isGrounded);
    }
}
