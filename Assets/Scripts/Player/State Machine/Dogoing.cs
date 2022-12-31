﻿using Helpers;

namespace Player
{
    public partial class PlayerStateMachine
    {
        public class Dogoing : PlayerState
        {
            public override void Enter(PlayerStateInput i)
            {
                i.oldVelocity = PlayerActions.Dogo();
                i.dogoXVBufferTimer = GameTimer.StartNewTimer(PlayerCore.DogoConserveXVTime);
            }

            public override void JumpPressed()
            {
                base.JumpPressed();
                MySM.Transition<DogoJumping>();
            }

            public override void FixedUpdate()
            {
                GameTimer.FixedUpdate(Input.dogoXVBufferTimer);

                base.FixedUpdate();
            }

            public override void MoveX(int moveDirection)
            {
                UpdateSpriteFacing(moveDirection);
            }
        }
    }
}