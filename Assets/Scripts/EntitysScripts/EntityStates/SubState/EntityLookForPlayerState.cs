using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityLookForPlayerState : EntityGroundedState
{
    public EntityLookForPlayerState(Entity entity, EntityStateMachine stateMachine, string animBoolName, SO_EntityData entityData) : base(entity, stateMachine, animBoolName, entityData)
    {
    }

    public override void Enter()
    {
        base.Enter();

        entity.StartCoroutine(LookForPlayer());

        core.Movement.SetVelocityX(0f);
    }

    public override void Exit()
    {
        base.Exit();
        entity.StopCoroutine(LookForPlayer());
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        entity.Core.Movement.SetVelocityX(0f);
    }

    private IEnumerator LookForPlayer()
    {
        entity.Core.Movement.Flip();
        if (entity.Core.CollisionSenses.EntityMin || entity.Core.CollisionSenses.EntityMax) 
        {
            stateMachine.ChangeState(entity.DetectedState);
        } 
        else if (!entity.Core.CollisionSenses.EntityMin || !entity.Core.CollisionSenses.EntityMax)
        {
            yield return new WaitForSeconds(entityData.timeBetweenTurns);
            entity.Core.Movement.Flip();
            if (entity.Core.CollisionSenses.EntityMin || entity.Core.CollisionSenses.EntityMax)
            {
                stateMachine.ChangeState(entity.DetectedState);
            } 
            else if (!entity.Core.CollisionSenses.EntityMin && !entity.Core.CollisionSenses.EntityMax)
            {
                stateMachine.ChangeState(entity.IdleState);
            }
        } 

    }
}
