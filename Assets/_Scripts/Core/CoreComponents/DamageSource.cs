using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oisho.CoreSystem
{
    public class DamageSource : CoreComponent
    {
        //CoreComponents
        protected CoreComp<Movement> movement;
        //

        /*
        public Transform AttackPosition
        {
            get => GenericNotImplementedError<Transform>.TryGet(attackPosition, core.transform.parent.name);
            private set => attackPosition = value;
        }
        */

        //public float AttackDamage { get => attackDamage; set => attackDamage = value; }
        //public float AttackRadius { get => attackRadius; set => attackRadius = value; }
        //public LayerMask WhatIsEnemy { get => whatIsEnemy; set => whatIsEnemy = value; }

        [Header("Attack parameters:")]
        [SerializeField] private Transform attackPosition;
        [SerializeField] private float attackDamage;
        [SerializeField] private float attackRadius;
        [SerializeField] private LayerMask whatIsEnemy;

        [Header("KnockBack parameters:")]
        [SerializeField] private float strength = 10;
        [SerializeField] private Vector2 angle = new Vector2(2.0f, 2.0f);

        protected override void Awake()
        {
            base.Awake();

            movement = new CoreComp<Movement>(core);
        }

        public void TriggerAttack()
        {

            Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attackPosition.position, attackRadius, whatIsEnemy);

            foreach (Collider2D collider in detectedObjects)
            {
                IDamageable damageable = collider.GetComponentInChildren<IDamageable>();
                IKnockBackable knockbackable = collider.GetComponentInChildren<IKnockBackable>();

                if (damageable != null)
                {
                    damageable.TakingDamage(attackDamage);
                }

                if (knockbackable != null)
                {
                    knockbackable.KnockBack(angle, strength, movement.Comp.FacingDirection);
                }
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
