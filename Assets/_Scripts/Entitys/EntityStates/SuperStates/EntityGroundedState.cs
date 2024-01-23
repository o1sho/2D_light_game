using Oisho.CoreSystem;
using UnityEngine;

public class EntityGroundedState : EntityState
{

    //CoreComponents
    protected Movement Movement
    {
        get => movement ??= core.GetCoreComponent<Movement>();
    }
    private Movement movement;

    protected CollisionSenses CollisionSenses
    {
        get => collisionSenses ??= core.GetCoreComponent<CollisionSenses>();
    }
    private CollisionSenses collisionSenses;

    private Combat Combat
    {
        get => combat ??= core.GetCoreComponent<Combat>();
    }
    private Combat combat;
    //


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

        if (Combat.Damaged)
        {
            stateMachine.ChangeState(entity.TakingDamageState);
        }
    }
}
