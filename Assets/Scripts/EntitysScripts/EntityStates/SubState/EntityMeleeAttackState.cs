using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EntityMeleeAttackState : EntityAbilityState
{

    public EntityMeleeAttackState(Entity entity, EntityStateMachine stateMachine, string animBoolName, SO_EntityData entityData) : base(entity, stateMachine, animBoolName, entityData)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();

        isAbilityDone = true;
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();

        TriggerAttack();
    }

    public override void Enter()
    {
        base.Enter();

        Movement?.SetVelocityX(0f);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        Movement?.SetVelocityX(0f);

    }

    private void TriggerAttack()
    {

        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(Combat.AttackPosition.position, Combat.AttackRadius, Combat.WhatIsEnemy);

        foreach (Collider2D collider in detectedObjects)
        {
            IDamageable damageable = collider.GetComponentInChildren<IDamageable>();
            IKnockbackable knockbackable = collider.GetComponentInChildren<IKnockbackable>();

            if (damageable != null)
            {
                damageable.TakingDamage(Combat.AttackDamage);
            }

            if (knockbackable != null)
            {
                knockbackable.Knockback(entityData.angle, entityData.strength, Movement.FacingDirection);
            }
        }
    }
}
