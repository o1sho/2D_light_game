using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerState
{
    //Inputs
    private int xInput;
    private bool jumpInput;
    private bool grabInput;
    private bool coyoteTime;
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


    public PlayerInAirState(Player player, PlayerStateMachine stateMachine, SO_PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
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

        //isTouchingWall= false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        CheckCoyoteTime();

        xInput = player.InputHandler.NormInputX;
        jumpInput = player.InputHandler.JumpInput;
        grabInput = player.InputHandler.GrabInput;

        if (player.InputHandler.AttackInputs[(int)CombatInputs.primary])
        {
            stateMachine.ChangeState(player.PrimaryAttackState);
        }
        else if (player.InputHandler.AttackInputs[(int)CombatInputs.secondary])
        {
            stateMachine.ChangeState(player.SecondaryAttackState);
        }

        else if (isGrounded && Movement.CurrentVelocity.y < 0.01f)
        {
            stateMachine.ChangeState(player.LandState);
        }
        else if (isTouchingWall && !isTouchingLedgeHorizontal && !isGrounded)
        {
            stateMachine.ChangeState(player.LedgeClimbState);
        }
        else if(jumpInput && player.JumpState.CanJump())//
        {
            player.InputHandler.UseJumpInput();/////
            stateMachine.ChangeState(player.JumpState);
        }
        else if (isTouchingWall && grabInput && isTouchingLedgeHorizontal)
        {
            stateMachine.ChangeState(player.WallGrabState);
        }
        else if (isTouchingWall && xInput == Movement.FacingDirection && Movement.CurrentVelocity.y <= 0)
        {
            stateMachine.ChangeState(player.WallSlideState);
        } 
        else
        {
            Movement?.CheckItShouldFlip(xInput);
            Movement?.SetVelocityX(playerData.movementVelocity * xInput);

            player.Animator.SetFloat("yVelocity", Movement.CurrentVelocity.y);
            player.Animator.SetFloat("xVelocity", Mathf.Abs(Movement.CurrentVelocity.x));
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private void CheckCoyoteTime()
    {
        if (coyoteTime && Time.time > startTime + playerData.coyoteTime)
        {
            coyoteTime = false;
            player.JumpState.DecreaseAmountOfJumpsLeft();
        }
    }

    public void StartCoyoteTime() => coyoteTime = true;
}
