using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityDetectedState : EntityGroundedState
{
    protected SO_EntityDetectedStateData stateData;
    public EntityDetectedState(Entity entity, EntityStateMachine stateMachine, string animBoolName, SO_EntityDetectedStateData stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }
}
