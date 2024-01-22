using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTouchingWallState : PlayerState
{
    //Input
    protected bool grabInput;
    protected int xInput;
    protected int yInput;
    //

    //Checks
    private bool isGrounded;
    private bool isTouchingWall;
    private bool isTouchingLedgeHorizontal;
    //

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
    //


    public PlayerTouchingWallState(Player player, PlayerStateMachine stateMachine, SO_PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }

    public override void DoChecks()
    {
        base.DoChecks();

        if (CollisionSenses)
        {
            isGrounded = CollisionSenses.Ground;
            isTouchingWall = CollisionSenses.Wall;
            isTouchingLedgeHorizontal = CollisionSenses.LedgeHorizontal;
        }

        if (isTouchingWall && !isTouchingLedgeHorizontal)
        {
            player.LedgeClimbState.SetDetectedPosition(player.transform.position);
        }
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        xInput = player.InputHandler.NormInputX;
        yInput = player.InputHandler.NormInputY;
        grabInput = player.InputHandler.GrabInput;

        if (isGrounded && !grabInput)
        {
            stateMachine.ChangeState(player.IdleState);
        } 
        else if (!isTouchingWall || (xInput != Movement.FacingDirection && !grabInput))
        {
            stateMachine.ChangeState(player.InAirState);
        }
        else if (isTouchingWall && !isTouchingLedgeHorizontal)
        {
            stateMachine.ChangeState(player.LedgeClimbState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
