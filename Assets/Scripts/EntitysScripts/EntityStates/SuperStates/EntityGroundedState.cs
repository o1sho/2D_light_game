using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityGroundedState : EntityState
{
    //Checks
    private bool isGrounded;
    private bool isTouchingWall;
    private bool isTouchingLedge;
    private bool isTouchingCeiling;

    public EntityGroundedState(Entity entity, EntityStateMachine stateMachine, string animBoolName) : base(entity, stateMachine, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isGrounded = core.CollisionSenses.Ground;
        isTouchingWall = core.CollisionSenses.Wall;
        isTouchingLedge = core.CollisionSenses.Ledge;
        isTouchingCeiling = core.CollisionSenses.Ceiling;
    }
}
