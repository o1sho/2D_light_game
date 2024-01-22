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

        Movement?.SetVelocityX(0f);
    }

    public override void Exit()
    {
        base.Exit();
        entity.StopCoroutine(LookForPlayer());
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        Movement?.SetVelocityX(0f);
    }

    private IEnumerator LookForPlayer()
    {
        Movement?.Flip();
        if (CollisionSenses.EntityMin || CollisionSenses.EntityMax) 
        {
            stateMachine.ChangeState(entity.DetectedState);
        } 
        else if (!CollisionSenses.EntityMin || !CollisionSenses.EntityMax)
        {
            if (CollisionSenses.EntityMin || CollisionSenses.EntityMax)
            {
                stateMachine.ChangeState(entity.DetectedState);
            }
            yield return new WaitForSeconds(entityData.timeBetweenTurns);
            if (CollisionSenses.EntityMin || CollisionSenses.EntityMax)
            {
                stateMachine.ChangeState(entity.DetectedState);
            }
            else if (!CollisionSenses.EntityMin || !CollisionSenses.EntityMax)
            {
                Movement?.Flip();
                if (CollisionSenses.EntityMin || CollisionSenses.EntityMax)
                {
                    stateMachine.ChangeState(entity.DetectedState);
                }
                else if (!CollisionSenses.EntityMin && !CollisionSenses.EntityMax)
                {
                    stateMachine.ChangeState(entity.IdleState);
                }
            }
        } 

    }
}
