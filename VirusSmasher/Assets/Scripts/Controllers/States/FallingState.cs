using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingState : Airborne
{
    public FallingState(PlayerController player) : base("FallingState",player) 
    {
    }

    public override void HandleInput()
    {
        base.HandleInput();

        if (player.IsGrounded())
            player.ChangeState(input.IsMoving ? player.walkingState : player.idleState);
    }
}
