using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityDetectedState : EntityState
{
    public EntityDetectedState(Entity entity, EntityStateMachine stateMachine, string animBoolName, SO_EntityData entityData) : base(entity, stateMachine, animBoolName, entityData)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        core.Movement.SetVelocityX(0);


        if (entity.Core.CollisionSenses.EntityMax && !entity.Core.CollisionSenses.EntityMin)
        {
            stateMachine.ChangeState(entity.ChargeState);
        }
        else if (entity.Core.CollisionSenses.EntityMin)
        {
            stateMachine.ChangeState(entity.MeleeAttackState);
        }
        else if (!entity.Core.CollisionSenses.EntityMax)
        {
            stateMachine.ChangeState(entity.LookForPlayerState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
