using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Combat : CoreComponent, IDamageable
{
    //[SerializeField] private GameObject hitParticles;

    public Transform AttackPosition
    {
        get => GenericNotImplementedError<Transform>.TryGet(attackPosition, core.transform.parent.name);
        private set => attackPosition = value;
    }

    public float AttackDamage { get => attackDamage; set => attackDamage = value; }
    public float AttackRadius { get => attackRadius; set => attackRadius = value; }
    public LayerMask WhatIsEnemy { get => whatIsEnemy; set => whatIsEnemy = value; }

    [SerializeField] private Transform attackPosition;

    [SerializeField] private float attackDamage;

    [SerializeField] private float attackRadius;

    [SerializeField] private LayerMask whatIsEnemy;

    public void Damage(float amount)
    {
        Debug.Log(core.transform.parent.name + " Damaged! " + amount + " Damage taken");
        //Instantiate(hitParticles, transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
        //StateMachine.ChangeState(TakingDamageState);
    }

    #region Gizmos
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPosition.position, attackRadius);
    }
    #endregion
}
