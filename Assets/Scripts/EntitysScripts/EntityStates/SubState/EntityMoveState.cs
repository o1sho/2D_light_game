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

        entity.Core.Movement.SetVelocityX(entityData.movementSpeed * entity.Core.Movement.FacingDirection);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        entity.Core.Movement.SetVelocityX(entityData.movementSpeed * entity.Core.Movement.FacingDirection);

        if (entity.Core.CollisionSenses.EntityMin || entity.Core.CollisionSenses.EntityMax && entity.Behavior == "agressive")
        {
            stateMachine.ChangeState(entity.DetectedState);
        }
        else if (entity.Core.CollisionSenses.Wall || !entity.Core.CollisionSenses.Ground)
        {
            stateMachine.ChangeState(entity.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
