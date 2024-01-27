using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityDetectedState : EntityGroundedState
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


        if (CollisionSenses.EntityMax && !CollisionSenses.EntityMin)
        {
            stateMachine.ChangeState(entity.ChargeState);
        }
        else if (CollisionSenses.EntityMin)
        {
            stateMachine.ChangeState(entity.MeleeAttackState);
        }
        else if (!CollisionSenses.EntityMax)
        {
            stateMachine.ChangeState(entity.LookForPlayerState);
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
