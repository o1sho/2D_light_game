using Oisho.CoreSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityAbilityState : EntityState
{
    protected bool isAbilityDone;

    //CoreComponents
    private Movement movement;
    protected Movement Movement => movement ?? core.GetCoreComponent<Movement>();

    private CollisionSenses collisionSenses;
    protected CollisionSenses CollisionSenses => collisionSenses ?? core.GetCoreComponent<CollisionSenses>();

    private TakingDamageReceiver takingDamageReceiver;
    protected TakingDamageReceiver TakingDamageReceiver => takingDamageReceiver ?? core.GetCoreComponent<TakingDamageReceiver>();

    private DamageSource damageSource;
    protected DamageSource DamageSource => damageSource ?? core.GetCoreComponent<DamageSource>();
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

        if (TakingDamageReceiver.Damaged)
        {
            stateMachine.ChangeState(entity.TakingDamageState);
        }
    }


}
