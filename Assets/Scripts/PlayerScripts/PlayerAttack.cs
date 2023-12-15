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
    [SerializeField] Transform attackPointLeft;
    [SerializeField] Transform attackPointRight;
    [SerializeField] Transform attackPointUp;
    [SerializeField] Transform attackPointDown;
    public float attackRange;
    [SerializeField] LayerMask enemyLayers;

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

            Collider2D[] hitEnemies  = Physics2D.OverlapCircleAll(attackPointLeft.position, attackRange, enemyLayers);
            foreach (Collider2D enemy in hitEnemies)
            {
                Debug.Log("Hit enemy " + enemy.name);
            }
        }
        if (_attackDirection == new Vector2(1, 0))
        {
            Debug.Log(_attackDirection);
            Debug.Log("RightAttack!");

            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPointRight.position, attackRange, enemyLayers);
            foreach (Collider2D enemy in hitEnemies)
            {
                Debug.Log("Hit enemy " + enemy.name);
            }
        }
        if (_attackDirection == new Vector2(0, 1))
        {
            Debug.Log(_attackDirection);
            Debug.Log("UpAttack!");

            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPointUp.position, attackRange, enemyLayers);
            foreach (Collider2D enemy in hitEnemies)
            {
                Debug.Log("Hit enemy " + enemy.name);
            }
        }
        if (_attackDirection == new Vector2(0, -1))
        {
            Debug.Log(_attackDirection);
            Debug.Log("DownAttack!");

            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPointDown.position, attackRange, enemyLayers);
            foreach (Collider2D enemy in hitEnemies)
            {
                Debug.Log("Hit enemy " + enemy.name);
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (attackPointLeft == null)
        {
            return;
        }
        if (attackPointRight == null)
        {
            return;
        }
        if (attackPointUp == null)
        {
            return;
        }
        if (attackPointDown == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPointLeft.position, attackRange);
        Gizmos.DrawWireSphere(attackPointRight.position, attackRange);
        Gizmos.DrawWireSphere(attackPointUp.position, attackRange);
        Gizmos.DrawWireSphere(attackPointDown.position, attackRange);
    }
}
