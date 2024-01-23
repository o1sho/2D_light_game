using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Oisho.CoreSystem
{
    public class Combat : CoreComponent, IDamageable, IKnockbackable
    {
        [SerializeField] private GameObject hitParticles;

        private bool isKnockbackActive;
        private float knockbackStartTime;

        //CoreComponents
        protected Movement Movement
        {
            get => movement ??= core.GetCoreComponent<Movement>();
        }
        private Movement movement;

        private CollisionSenses CollisionSenses
        {
            get => collisionSenses ??= core.GetCoreComponent<CollisionSenses>();
        }
        private CollisionSenses collisionSenses;

        private Stats Stats
        {
            get => stats ??= core.GetCoreComponent<Stats>();
        }
        private Stats stats;

        private ParticleManager ParticleManager => particleManager ? particleManager : core.GetCoreComponent<ParticleManager>();
        private ParticleManager particleManager;
        //

        public override void LogicUpdate()
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
            ParticleManager?.StartParticlesWithRandomRotation(hitParticles);
            Stats?.DecreaseHealth(amount);
        }

        public void Knockback(Vector2 angle, float strength, int direction)
        {
            Movement?.SetVelocity(angle, strength, direction);
            Movement.CanSetVelocity = false;
            isKnockbackActive = true;
            knockbackStartTime = Time.time;
        }

        private void CheckKnockback()
        {
            if (isKnockbackActive && Movement.CurrentVelocity.y <= 0.01f && CollisionSenses.Ground)
            {
                isKnockbackActive = false;
                Movement.CanSetVelocity = true;
            }
        }

        #region Gizmos
        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(attackPosition.position, attackRadius);
        }
        #endregion
    }
}

