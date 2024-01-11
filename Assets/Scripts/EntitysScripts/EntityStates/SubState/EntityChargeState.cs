using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityChargeState : EntityState
{
    private bool isChargeTimeOver;

    public EntityChargeState(Entity entity, EntityStateMachine stateMachine, string animBoolName, SO_EntityData entityData) : base(entity, stateMachine, animBoolName, entityData)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        isChargeTimeOver = false;
        
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        entity.Core.Movement.SetVelocityX(entityData.chargeSpeed * entity.Core.Movement.FacingDirection);

        if (Time.time >= startTime + entityData.chargeTime)
        {
            isChargeTimeOver = true;
        }

        if (entity.Core.CollisionSenses.EntityMin)
        {
            stateMachine.ChangeState(entity.MeleeAttackState);
        }
        else if (entity.Core.CollisionSenses.Wall || !entity.Core.CollisionSenses.Ground)
        {
            stateMachine.ChangeState(entity.IdleState);
        }
        else if (isChargeTimeOver && !entity.Core.CollisionSenses.EntityMax)
        {
            stateMachine.ChangeState(entity.LookForPlayerState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
