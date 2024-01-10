using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityState
{
    protected Core core;

    protected Entity entity;
    protected EntityStateMachine stateMachine;
    protected SO_EntityData entityData;

    protected float startTime;

    protected bool isAnimationFinished;

    private string animBoolName;


    public EntityState(Entity entity, EntityStateMachine stateMachine, string animBoolName, SO_EntityData entityData)
    {
        this.entity = entity;
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
        this.entityData = entityData;
        core = entity.Core;
    }

    public virtual void Enter()
    {
        startTime = Time.time;
        entity.Animator.SetBool(animBoolName, true);
        Debug.Log("The entity is in a state: " + animBoolName);
        isAnimationFinished = false;
    }

    public virtual void Exit()
    {
        entity.Animator.SetBool(animBoolName, false);
    }

    public virtual void LogicUpdate()
    {

    }

    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }

    public virtual void DoChecks() { }
    public virtual void AnimationTrigger() { }

    public virtual void AnimationFinishTrigger() => isAnimationFinished = true;
}
