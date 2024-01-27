using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityChargeState : EntityGroundedState
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

        Movement?.SetVelocityX(entityData.chargeSpeed * Movement.FacingDirection);

        if (Time.time >= startTime + entityData.chargeTime)
        {
            isChargeTimeOver = true;
        }

        if (CollisionSenses.EntityMin)
        {
            stateMachine.ChangeState(entity.MeleeAttackState);
        }
        else if (CollisionSenses.Wall || !CollisionSenses.LedgeVertical)
        {
            stateMachine.ChangeState(entity.IdleState);
            Debug.Log("ya durak");
        }
        else if (isChargeTimeOver && !CollisionSenses.EntityMax)
        {
            stateMachine.ChangeState(entity.LookForPlayerState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
