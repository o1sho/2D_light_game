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

        Movement?.SetVelocityX(0f);

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
        if (CollisionSenses.Wall || !CollisionSenses.LedgeVertical)
        {
            Movement?.Flip();
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time >= startTime + idleTime) isIdleTimeOver = true;

        Movement?.SetVelocityX(0f);

        if (isIdleTimeOver)
        {
            stateMachine.ChangeState(entity.MoveState);
        }
        if (CollisionSenses.EntityMax && entity.Behavior == "agressive")
        {
            stateMachine.ChangeState(entity.DetectedState);
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
