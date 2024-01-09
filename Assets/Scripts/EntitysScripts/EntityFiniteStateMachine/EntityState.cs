using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityState
{
    protected Core core;

    protected Entity entity;
    protected EntityStateMachine stateMachine;

    protected float startTime;

    private string animBoolName;

    public EntityState(Entity entity, EntityStateMachine stateMachine, string animBoolName)
    {
        this.entity = entity;
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
        core = entity.Core;
    }

    public virtual void Enter()
    {
        startTime = Time.time;
        entity.Animator.SetBool(animBoolName, true);
        Debug.Log("The entity is in a state: " + animBoolName);
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
}
