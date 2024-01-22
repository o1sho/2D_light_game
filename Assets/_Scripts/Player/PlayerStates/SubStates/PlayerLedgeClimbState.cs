using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerLedgeClimbState : PlayerState
{
    private Vector2 detectedPos;
    private Vector2 cornerPos;
    private Vector2 startPos;
    private Vector2 stopPos;
    private Vector2 workspace;

    private bool isHanding;
    private bool isClimbing;

    //Input
    private int xInput;
    private int yInput;
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


    public PlayerLedgeClimbState(Player player, PlayerStateMachine stateMachine, SO_PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();

        player.Animator.SetBool("climbLedge", false);
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();

        isHanding = true;
    }

    public override void DoChecks()
    {
        base.DoChecks();

    }

    public override void Enter()
    {
        base.Enter();

        Movement?.SetVelocityZero();
        //player.transform.position = detectedPos;
        cornerPos = DetermineCornerPosition();

        startPos.Set(cornerPos.x - (Movement.FacingDirection * playerData.startOffset.x), cornerPos.y - playerData.startOffset.y);
        stopPos.Set(cornerPos.x + (Movement.FacingDirection * playerData.stopOffset.x), cornerPos.y + playerData.stopOffset.y);

        player.transform.position = startPos;
    }

    public override void Exit()
    {
        base.Exit();

        isHanding = false;

        if (isClimbing)
        {
            player.transform.position = stopPos;
            isClimbing = false;
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isAnimationFinished)
        {
            stateMachine.ChangeState(player.IdleState);
        }
        else
        {
            xInput = player.InputHandler.NormInputX;
            yInput = player.InputHandler.NormInputY;

            Movement?.SetVelocityZero();
            player.transform.position = startPos;

            if (xInput == Movement.FacingDirection && isHanding && !isClimbing)
            {
                isClimbing = true;
                player.Animator.SetBool("climbLedge", true);
            }
            else if (yInput == -1 && isHanding && !isClimbing)
            {
                stateMachine.ChangeState(player.InAirState);
            }
        }
    }

    public void SetDetectedPosition(Vector2 pos) => detectedPos = pos;

    private Vector2 DetermineCornerPosition()
    {
        RaycastHit2D xHit = Physics2D.Raycast(CollisionSenses.WallCheck.position, Vector2.right * Movement.FacingDirection, CollisionSenses.WallCheckDistance, CollisionSenses.WhatIsGround);
        float xDistance = xHit.distance;
        workspace.Set((xDistance + 0.015f) * Movement.FacingDirection, 0f);
        RaycastHit2D yHit = Physics2D.Raycast(CollisionSenses.LedgeCheckHorizontal.position + (Vector3)(workspace), Vector2.down, CollisionSenses.LedgeCheckHorizontal.position.y - CollisionSenses.WallCheck.position.y + 0.015f, CollisionSenses.WhatIsGround);
        float yDistance = yHit.distance;

        workspace.Set(CollisionSenses.WallCheck.position.x + (xDistance * Movement.FacingDirection), CollisionSenses.LedgeCheckHorizontal.position.y - yDistance);
        return workspace;
    }


}
