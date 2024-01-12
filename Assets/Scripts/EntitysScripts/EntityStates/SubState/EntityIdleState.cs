using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class EntityIdleState : EntityGroundedState
{
    private float idleTime;

    private bool flipAfterIdle;
    private bool isIdleTimeOver;

    public EntityIdleState(Entity entity, EntityStateMachine stateMachine, string animBoolName, SO_EntityData entityData) : base(entity, stateMachine, animBoolName, entityData)
    {
    }

    public override void Enter()
    {
        base.Enter();

        core.Movement.SetVelocityX(0f);

        isIdleTimeOver = false;
        SetRandomIdleTime();
    }

    public override void Exit()
    {
        base.Exit();

        /*
        if (flipAfterIdle)
        {
            entity.Core.Movement.Flip();
        }
        */
        if (entity.Core.CollisionSenses.Wall || !entity.Core.CollisionSenses.LedgeVertical)
        {
            entity.Core.Movement.Flip();
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time >= startTime + idleTime) isIdleTimeOver = true;

        core.Movement.SetVelocityX(0f);

        if (isIdleTimeOver)
        {
            stateMachine.ChangeState(entity.MoveState);
        }

    }
    
    private void SetFpipAfterIdle(bool flip)
    {
        flipAfterIdle = flip;
    }

    private void SetRandomIdleTime()
    {
        idleTime = Random.Range(entityData.minIdleTime, entityData.maxIdleTime);
    }
}
