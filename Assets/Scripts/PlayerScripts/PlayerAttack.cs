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

    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
        _inputManager = GetComponent<InputManager>();
    }

    private void Start()
    {
        _inputManager.inputController.Player.Attack.performed += context => OnAttack();
    }

    private void OnAttack()
    {
        Debug.Log("Player Attack!");
    }
}
