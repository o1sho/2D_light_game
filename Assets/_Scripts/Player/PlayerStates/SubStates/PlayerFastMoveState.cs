using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oisho
{
    public class PlayerFastMoveState : PlayerGroundedState
    {
        public PlayerFastMoveState(Player player, PlayerStateMachine stateMachine, SO_PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
        {
        }

        public override void DoChecks()
        {
            base.DoChecks();
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

            Movement?.CheckItShouldFlip(xInput);

            Movement?.SetVelocityX(playerData.fastMovementVelocity * xInput);


            if (!isExitingState)
            {
                if (xInput == 0)
                {
                    stateMachine.ChangeState(player.IdleState);
                }
                else if (!fastMoveInput)
                {
                    stateMachine.ChangeState(player.MoveState);
                }
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
    }
}
