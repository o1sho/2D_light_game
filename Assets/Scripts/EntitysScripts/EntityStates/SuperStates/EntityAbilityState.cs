using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityAbilityState : EntityState
{
    protected bool isAbilityDone;

    public EntityAbilityState(Entity entity, EntityStateMachine stateMachine, string animBoolName, SO_EntityData entityData) : base(entity, stateMachine, animBoolName, entityData)
    {
    }

    public override void Enter()
    {
        base.Enter();

        isAbilityDone = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        if (isAbilityDone)
        {
            stateMachine.ChangeState(entity.MoveState);
        }

        if (entity.Core.Combat.Damaged)
        {
            stateMachine.ChangeState(entity.TakingDamageState);
        }
    }
}
