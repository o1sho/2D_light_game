using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerRollState : PlayerAbilityState
{
    private Vector2 rollDirection;
    public PlayerRollState(Player player, PlayerStateMachine stateMachine, SO_PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.InputHandler.UseRollInput();
        rollDirection = Vector2.right * core.Movement.FacingDirection;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        core.Movement.SetVelocityX(playerData.rollVelocity * rollDirection.x);

        if (Time.time >= startTime + playerData.rollTime)
        {
            isAbilityDone = true;
        }
    }
}
