using UnityEditor.Rendering.LookDev;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(InputManager))]
public class PlayerAttack : MonoBehaviour
{
    [Header("Player attack stats:")]
    [SerializeField] private float _attackSpeed;
    [SerializeField] private int _attackDamage;

    [Header("Player components:")]
    private PlayerController _playerController;
    private InputManager _inputManager;

    [SerializeField] Transform attackPoint;
    public float attackRange;
    [SerializeField] LayerMask enemyLayers;

    private Animator _animator;

    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
        _inputManager = GetComponent<InputManager>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        //_inputManager.inputController.Player.Attack.performed += context => OnAttack();
    }

    private void Update()
    {
    }

    private void OnAttack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("Hit enemy " + enemy.name);
            enemy.GetComponent<EnemyHp>().SetEnemyHp(_attackDamage);
        }
        _animator.SetTrigger("isAttacking");
    }

    private void OnDrawGizmos()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
