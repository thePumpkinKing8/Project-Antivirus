using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Airborne : BaseState
{
    protected Airborne(string name, PlayerController player) : base(name, player)
    {
    }

    public override void HandleInput()
    {
        base.HandleInput();

        if (InputManager.Dash.triggered && player.CanDash())
            ChangeState(player.dashState);
    }

    public override void UpdateState()
    {
        base.UpdateState();

        Move();
    }

    private void Move()
    {
        player._rb.velocity = new Vector2(input.MoveInput.x * player.settings.movementSpeed, player._rb.velocity.y);
    }
}
