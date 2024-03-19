using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grounded : BaseState
{
    protected Grounded(string name, PlayerController player) : base(name, player) 
    {
    }

    public override void HandleInput()
    {
        base.HandleInput();

        if (input.IsFalling)
            player.ChangeState(player.fallingState);

        if(InputManager.Jump.triggered)
            ChangeState(player.jumpState);
        

        if(InputManager.Dash.triggered && player.CanDash())
            ChangeState(player.dashState);
    }

}
