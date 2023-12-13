using UnityEngine;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(InputManager))]
public class PlayerAttack : MonoBehaviour
{
    [Header("Player attack stats:")]
    [SerializeField] private float _attackSpeed;
    [SerializeField] private float _attackDamage;

    [Header("Player components:")]
    private PlayerController _playerController;
    private InputManager _inputManager;

    private Vector2 _attackDirection;

    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
        _inputManager = GetComponent<InputManager>();
    }

    private void Start()
    {

    }

    private void Update()
    {
        _attackDirection = _inputManager.inputController.Player.Attack.ReadValue<Vector2>();

        OnAttack();
        //Debug.Log(_attackDirection);
    }

    private void OnAttack()
    {
        if (_attackDirection == new Vector2(-1,0))
        {
            Debug.Log(_attackDirection);
            Debug.Log("LeftAttack!");
        }
        if (_attackDirection == new Vector2(1, 0))
        {
            Debug.Log(_attackDirection);
            Debug.Log("RightAttack!");
        }
        if (_attackDirection == new Vector2(0, 1))
        {
            Debug.Log(_attackDirection);
            Debug.Log("UpAttack!");
        }
        if (_attackDirection == new Vector2(0, -1))
        {
            Debug.Log(_attackDirection);
            Debug.Log("DownAttack!");
        }
    }
}
