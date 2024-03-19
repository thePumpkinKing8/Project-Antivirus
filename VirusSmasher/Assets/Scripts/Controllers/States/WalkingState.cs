using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingState : Grounded
{
    public WalkingState(PlayerController player) : base("WalkingState", player)
    {
    }

    public override void HandleInput()
    {
        base.HandleInput();

        if (input.IsIdle)
            player.ChangeState(player.idleState);
        else if (input.IsDashing)
            player.ChangeState(player.dashState);
            
    }

    public override void UpdateState()
    {
        base.UpdateState();

        player._rb.velocity = new Vector2(input.MoveInput.x * player.settings.movementSpeed, player._rb.velocity.y);
    }
}
