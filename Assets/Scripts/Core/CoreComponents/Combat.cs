using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Combat : CoreComponent, IDamageable, IKnockbackable
{
    [SerializeField] private GameObject hitParticles;

    private bool isKnockbackActive;
    private float knockbackStartTime;

    public void LogicUpdate()
    {
        CheckKnockback();
    }

    public Transform AttackPosition
    {
        get => GenericNotImplementedError<Transform>.TryGet(attackPosition, core.transform.parent.name);
        private set => attackPosition = value;
    }

    public float AttackDamage { get => attackDamage; set => attackDamage = value; }
    public float AttackRadius { get => attackRadius; set => attackRadius = value; }
    public LayerMask WhatIsEnemy { get => whatIsEnemy; set => whatIsEnemy = value; }
    public bool Damaged { get => damaged; set => damaged = value; }

    [SerializeField] private Transform attackPosition;

    [SerializeField] private float attackDamage;

    [SerializeField] private float attackRadius;

    [SerializeField] private LayerMask whatIsEnemy;
    [SerializeField] bool damaged;

    public void TakingDamage(float amount)
    {
        damaged = true;
        Debug.Log(core.transform.parent.name + " Damaged! " + amount + " Damage taken");
        Instantiate(hitParticles, transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
    }

    public void Knockback(Vector2 angle, float strength, int direction)
    {
        core.Movement.SetVelocity(strength, angle, direction);
        core.Movement.CanSetVelocity = false;
        isKnockbackActive = true;
        knockbackStartTime= Time.time;
    }

    private void CheckKnockback()
    {
        if (isKnockbackActive && core.Movement.CurrentVelocity.y <= 0.01f && core.CollisionSenses.Ground)
        {
            isKnockbackActive= false;
            core.Movement.CanSetVelocity = true;
        }
    }

    #region Gizmos
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPosition.position, attackRadius);
    }
    #endregion
}
