using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : Airborne
{
    public JumpState(PlayerController player) : base("JumpingState", player)
    {
    }

    public override void EnterState()
    {
        input.IsJumping = true;

        Jump();
    }

    private void Jump()
    {
        if (!player.IsGrounded()) return;

        player._rb.velocity = new Vector2(player._rb.velocity.x, player.settings.jumpHeight);
    }

    public override void HandleInput()
    {
        base.HandleInput();

        if (input.IsFalling)
            player.ChangeState(player.fallingState);
    }

    public override void ExitState()
    {
        input.IsJumping = false;
    }
}
