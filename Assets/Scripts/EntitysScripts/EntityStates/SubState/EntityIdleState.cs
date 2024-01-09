using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class EntityIdleState : EntityGroundedState
{
    protected SO_EntityIdleStateData stateData;

    private float idleTime;

    private bool flipAfterIdle;
    private bool isIdleTimeOver;

    public EntityIdleState(Entity entity, EntityStateMachine stateMachine, string animBoolName, SO_EntityIdleStateData stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();

        core.Movement.SetVelocityX(0);

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
        entity.Core.Movement.Flip();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time >= startTime + idleTime) isIdleTimeOver = true;

        core.Movement.SetVelocityX(0);

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
        idleTime = Random.Range(stateData.minIdleTime, stateData.maxIdleTime);
    }
}
