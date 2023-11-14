using UnityEngine;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(InputManager))]
public class PlayerBlock : MonoBehaviour
{
    [Header("Player block stats:")]
    [SerializeField] private float _blockSpeed;

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
        _inputManager.inputController.Player.Block.performed += context => OnBlock();
    }

    private void OnBlock()
    {
        Debug.Log("Player Block!");
    }
}
