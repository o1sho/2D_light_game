using Oisho.CoreSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    //Input
    protected int xInput;
    private bool jumpInput;
    private bool rollInput;
    private bool grabInput;
    protected bool fastMoveInput;
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

    protected CollisionSenses CollisionSenses
    {
        get => collisionSenses ??= core.GetCoreComponent<CollisionSenses>();
    }
    private CollisionSenses collisionSenses;
    //


    public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, SO_PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
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
    }

    public override void Enter()
    {
        base.Enter();

        player.JumpState.ResetAmountOfJumpsLeft();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        xInput = player.InputHandler.NormInputX;
        jumpInput = player.InputHandler.JumpInput;
        rollInput = player.InputHandler.RollInput;
        grabInput= player.InputHandler.GrabInput;
        fastMoveInput= player.InputHandler.FastMoveInput;

        if (player.InputHandler.AttackInputs[(int)CombatInputs.primary])
        {
            stateMachine.ChangeState(player.PrimaryAttackState);
        } 
        else if (player.InputHandler.AttackInputs[(int)CombatInputs.secondary])
        {
            stateMachine.ChangeState(player.SecondaryAttackState);
        }

        else if (jumpInput && player.JumpState.CanJump())//
        {
            player.InputHandler.UseJumpInput();
            stateMachine.ChangeState(player.JumpState);
        } 
        else if (rollInput)
        {
            stateMachine.ChangeState(player.RollState);
        }
        else if (!isGrounded)
        {
            player.InAirState.StartCoyoteTime();
            stateMachine.ChangeState(player.InAirState);
        } 
        else if (isTouchingWall && grabInput && isTouchingLedgeHorizontal)
        {
            stateMachine.ChangeState(player.WallGrabState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
