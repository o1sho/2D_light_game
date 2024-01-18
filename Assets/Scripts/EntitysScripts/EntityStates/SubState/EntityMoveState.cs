using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityMoveState : EntityGroundedState
{
    public EntityMoveState(Entity entity, EntityStateMachine stateMachine, string animBoolName, SO_EntityData entityData) : base(entity, stateMachine, animBoolName, entityData)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        Movement?.SetVelocityX(entityData.movementSpeed * Movement.FacingDirection);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        Movement?.SetVelocityX(entityData.movementSpeed * Movement.FacingDirection);

        if (CollisionSenses.EntityMax && entity.Behavior == "agressive")
        {
            stateMachine.ChangeState(entity.DetectedState);
        }
        else if (CollisionSenses.Wall || !CollisionSenses.LedgeVertical)
        {
            stateMachine.ChangeState(entity.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
