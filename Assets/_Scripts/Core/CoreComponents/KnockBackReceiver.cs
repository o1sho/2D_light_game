using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Oisho.CoreSystem
{
    public class KnockBackReceiver : CoreComponent, IKnockBackable
    {
        [SerializeField] private float maxKnockBackTime = 0.2f;
        private bool isKnockBackActive;
        private float knockBackStartTime;

        //CoreComponents
        private CoreComp<Movement> movement;
        private CoreComp<CollisionSenses> collisionSenses;
        //

        protected override void Awake()
        {
            base.Awake();

            movement = new CoreComp<Movement>(core);
            collisionSenses = new CoreComp<CollisionSenses>(core);
        }

        public override void LogicUpdate()
        {
            CheckKnockback();
        }

        public void KnockBack(Vector2 angle, float strength, int direction)
        {
            movement.Comp?.SetVelocity(angle, strength, direction);
            movement.Comp.CanSetVelocity = false;
            isKnockBackActive = true;
            knockBackStartTime = Time.time;
        }

        private void CheckKnockback()
        {
            if (isKnockBackActive && ((movement.Comp.CurrentVelocity.y <= 0.01f && collisionSenses.Comp.Ground) || Time.time >= knockBackStartTime + maxKnockBackTime))
            {
                isKnockBackActive = false;
                movement.Comp.CanSetVelocity = true;
            }
        }
    }
}

