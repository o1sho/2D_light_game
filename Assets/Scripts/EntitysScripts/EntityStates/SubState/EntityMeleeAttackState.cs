using System.Collections;
using System.Collections.Generic;
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

        core.Movement.SetVelocityX(0f);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        core.Movement.SetVelocityX(0f);
        if (!entity.Core.CollisionSenses.EntityMin && !entity.Core.CollisionSenses.EntityMax)
        {
            stateMachine.ChangeState(entity.LookForPlayerState);
        }
    }

    private void TriggerAttack()
    {
        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(entity.Core.Combat.AttackPosition.position, entity.Core.Combat.AttackRadius, entity.Core.Combat.WhatIsEnemy);

        foreach (Collider2D collider in detectedObjects)
        {
            IDamageable damageable = collider.GetComponentInChildren<IDamageable>();

            if (damageable != null)
            {
                damageable.TakingDamage(entity.Core.Combat.AttackDamage);
            }
        }
    }

}
