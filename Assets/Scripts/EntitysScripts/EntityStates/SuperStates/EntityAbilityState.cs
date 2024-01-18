using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityAbilityState : EntityState
{
    protected bool isAbilityDone;

    //CoreComponents
    protected Movement Movement
    {
        get => movement ??= core.GetCoreComponent<Movement>();
    }
    private Movement movement;

    private CollisionSenses CollisionSenses
    {
        get => collisionSenses ??= core.GetCoreComponent<CollisionSenses>();
    }
    private CollisionSenses collisionSenses;

    protected Combat Combat
    {
        get => combat ??= core.GetCoreComponent<Combat>();
    }
    private Combat combat;
    //

    public EntityAbilityState(Entity entity, EntityStateMachine stateMachine, string animBoolName, SO_EntityData entityData) : base(entity, stateMachine, animBoolName, entityData)
    {
    }
    public override void DoChecks()
    {
        base.DoChecks();
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

        if (Combat.Damaged)
        {
            stateMachine.ChangeState(entity.TakingDamageState);
        }
    }


}
