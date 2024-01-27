using Oisho.CoreSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class EntityTakingDamageState : EntityAbilityState
{
    private float takingDamageTime;

    //private bool flipAfterIdle;
    private bool isTakingDamageTimeOver;

    public EntityTakingDamageState(Entity entity, EntityStateMachine stateMachine, string animBoolName, SO_EntityData entityData) : base(entity, stateMachine, animBoolName, entityData)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();

        isAbilityDone= true;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        Movement?.SetVelocityX(0f);
        isTakingDamageTimeOver = false;
        SetRandomTakingDamageTime();
    }

    public override void Exit()
    {
        base.Exit();

        TakingDamageReceiver.Damaged = false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time >= startTime + takingDamageTime) isTakingDamageTimeOver = true;

        Movement?.SetVelocityX(0);


        if (isTakingDamageTimeOver)
        {
            stateMachine.ChangeState(entity.DetectedState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private void SetRandomTakingDamageTime()
    {
        takingDamageTime = Random.Range(entityData.minTakingDamageTime, entityData.maxTakingDamageTime);
    }
}
