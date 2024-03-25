using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DashState : BaseState
{
    public DashState(PlayerController player) : base("DashState",player) 
    {
    }
    private float timer;
    public override void EnterState()
    {
        input.IsDashing = true;

        timer = 0;

        player.grounded = false;

    }

    public override void UpdateState()
    {
        base.UpdateState();
        timer += Time.deltaTime;
        if (timer > settings.dashTime)
            ChangeState(input.IsFalling ? player.fallingState : player.idleState);
        Move();
    }
    
    private void Move()
    {
        var direction = input.MoveInput;
        if (direction == Vector2.zero)
            direction = Vector2.right * player.lastDirection;
        player.transform.Translate(((direction * settings.dashDistance)/ settings.dashTime) * Time.deltaTime);
    }
    
    

    public override void ExitState()
    {
        input.IsDashing = false;
        player.TempCoolDownReset();
        if (player.IsGrounded())
            player.grounded = true;
        
    }
}
