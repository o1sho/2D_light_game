using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityGroundedState : EntityState
{

    public EntityGroundedState(Entity entity, EntityStateMachine stateMachine, string animBoolName, SO_EntityData entityData) : base(entity, stateMachine, animBoolName, entityData)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (entity.Core.Combat.Damaged)
        {
            stateMachine.ChangeState(entity.TakingDamageState);
        }
    }
}
