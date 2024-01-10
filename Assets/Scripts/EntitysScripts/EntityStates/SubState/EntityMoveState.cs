using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityMoveState : EntityGroundedState
{
    protected SO_EntityMoveStateData stateData;

    public EntityMoveState(Entity entity, EntityStateMachine stateMachine, string animBoolName, SO_EntityMoveStateData stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        entity.Core.Movement.SetVelocityX(stateData.movementSpeed * entity.Core.Movement.FacingDirection);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        entity.Core.Movement.SetVelocityX(stateData.movementSpeed * entity.Core.Movement.FacingDirection);

        if (entity.Core.CollisionSenses.EntityMin && entity.Behavior == "agressive")
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
