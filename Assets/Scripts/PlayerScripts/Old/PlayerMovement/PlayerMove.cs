using UnityEngine;

[RequireComponent(typeof(InputManager))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMove : MonoBehaviour
{
    [Header("PLAYER MOVE SETTINGS:")]
    [SerializeField, Range(1, 100)] private float _moveSpeed;

    private Vector2 _moveDirection;
    public static bool isFacingRight = true;
    private bool _isRuning;
    public static bool canMove=true;
    public static bool canFlip=true;

    //COMPONENTS:
    private InputManager _inputManager;
    private Rigidbody2D _rigidBody2D;
    private Animator _animator;

    private void Awake()
    {
        _inputManager= GetComponent<InputManager>();
        _rigidBody2D= GetComponent<Rigidbody2D>();
        _animator= GetComponent<Animator>();
    }
    private void Update()
    {
        CheckInput();
        CheckMovementDirections();
        CheckIsRuning();
    }
    private void FixedUpdate()
    {
        UpdateAnimations();
        if(canMove) Move();
    }

    private void CheckInput()
    {
        _moveDirection = _inputManager.inputController.Player.Move.ReadValue<Vector2>();
    }

    private void Move()
    {
        _rigidBody2D.velocity = new Vector2(_moveDirection.x * _moveSpeed, _rigidBody2D.velocity.y);
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    private void CheckMovementDirections()
    {
        if (isFacingRight && _moveDirection.x < 0)
        {
            if (canFlip) Flip();
        }
        else if (!isFacingRight && _moveDirection.x > 0)
        {
            if (canFlip) Flip();
        }
    }

    private void CheckIsRuning()
    {

        if (_moveDirection.x != 0)
        {
            _isRuning = true;
        }
        else
        {
            _isRuning = false;
        }
    }

    private void UpdateAnimations()
    {
        _animator.SetBool("isRuning", _isRuning);
    }
}
