using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHp : MonoBehaviour
{
    [SerializeField] private int _hp;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public int GetEnemyHp()
    {
        return _hp;
    }

    public void SetEnemyHp(int count)
    {
        _hp -= count;

        //Animator
        _animator.SetTrigger("isTakeDamage");
    }

    private void Update()
    {
        if (_hp <= 0)
        {
            Died();
        }
    }

    private void Died()
    {
        Destroy(gameObject);
    }
}
