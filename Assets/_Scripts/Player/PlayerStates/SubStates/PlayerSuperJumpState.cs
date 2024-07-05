using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oisho
{
    public class PlayerSuperJumpState : PlayerAbilityState
    {
        private int amountOfJumpsLeft;
        public PlayerSuperJumpState(Player player, PlayerStateMachine stateMachine, SO_PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
        {
            amountOfJumpsLeft = playerData.amountOfJumps;
        }

        public override void Enter()
        {
            base.Enter();

            Movement?.SetVelocityY(playerData.superJumpVelocity);
            isAbilityDone = true;
            amountOfJumpsLeft--;
        }

        public bool CanJump()
        {
            if (amountOfJumpsLeft > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void ResetAmountOfJumpsLeft() => amountOfJumpsLeft = playerData.amountOfJumps;

        public void DecreaseAmountOfJumpsLeft() => amountOfJumpsLeft--;
    }
}
