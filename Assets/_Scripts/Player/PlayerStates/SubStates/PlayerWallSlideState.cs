using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : PlayerTouchingWallState
{
    public PlayerWallSlideState(Player player, PlayerStateMachine stateMachine, SO_PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        Movement?.SetVelocityY(-playerData.wallSlideVelocity);

        if (grabInput && yInput == 0)
        {
            stateMachine.ChangeState(player.WallGrabState);
        }
    }
}
