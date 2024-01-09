using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class EntityTakingDamageState : EntityState
{
    protected SO_EntityTakingDamageStateData stateData;

    private float takingDamageTime;

    //private bool flipAfterIdle;
    private bool isTakingDamageTimeOver;

    public EntityTakingDamageState(Entity entity, EntityStateMachine stateMachine, string animBoolName, SO_EntityTakingDamageStateData stateData) : base(entity, stateMachine, animBoolName)
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

        entity.Core.Movement.SetVelocityX(0);
        isTakingDamageTimeOver = false;
        SetRandomTakingDamageTime();
    }

    public override void Exit()
    {
        base.Exit();

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time >= startTime + takingDamageTime) isTakingDamageTimeOver = true;
        entity.Core.Movement.SetVelocityX(0);
        if (isTakingDamageTimeOver) stateMachine.ChangeState(entity.MoveState);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private void SetRandomTakingDamageTime()
    {
        takingDamageTime = Random.Range(stateData.minTakingDamageTime, stateData.maxTakingDamageTime);
    }
}
