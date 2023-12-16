using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHp : MonoBehaviour
{
    [SerializeField] private int _hp;

    public int GetEnemyHp()
    {
        return _hp;
    }

    public void SetEnemyHp(int count)
    {
        _hp -= count;
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
