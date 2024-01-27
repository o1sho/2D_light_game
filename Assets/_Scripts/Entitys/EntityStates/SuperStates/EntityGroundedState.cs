using Oisho.CoreSystem;
using UnityEngine;

public class EntityGroundedState : EntityState
{

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

        if (TakingDamageReceiver.Damaged)
        {
            stateMachine.ChangeState(entity.TakingDamageState);
        }
    }
}
