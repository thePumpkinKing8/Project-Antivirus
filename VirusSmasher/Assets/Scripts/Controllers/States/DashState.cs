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
    [HideInInspector] public Vector3 storedPosition;
    public override void EnterState()
    {
        input.IsDashing = true;

        timer = 0;

        player.grounded = false;

        var direction = input.MoveInput;
        if (direction == Vector2.zero)
            direction = Vector2.right * player.lastDirection;

        AudioManager.Instance.PlayerPlay(player.dashSFX);


    }

    public override void UpdateState()
    {
        base.UpdateState();
        timer += Time.deltaTime;
    }

    public override void HandleMovement()
    {
        base.HandleMovement();
        if (timer > settings.dashTime)
        {
            ChangeState(input.IsFalling ? player.fallingState : player.idleState);
            return;
        }
            
        Move();

        
        if (Physics2D.OverlapBox(player.transform.position, new Vector2(1, 1), 0, settings.groundLayerMask))
        {
            player.transform.position = storedPosition;
            return;
        }
        else if (Physics2D.OverlapBox(player.transform.position, new Vector2(.5f, 1), 0, LayerMask.GetMask("Walls")))
        {
            player.transform.position = storedPosition;
            Debug.Log("hey");
            return;
        }

    }

    private void Move()
    {
        storedPosition = player.transform.position;
        var direction = input.MoveInput;
        if (direction == Vector2.zero)
            direction = Vector2.right * player.lastDirection;

        RaycastHit2D ray = Physics2D.Raycast(player.transform.position - new Vector3(direction.x, direction.y, 0) * .1f, direction, settings.dashDistance/settings.dashTime * Time.deltaTime, ~LayerMask.GetMask("Player"));
        if(ray.collider != null)
        {
            if (ray.collider.tag == "Terrain")
            {
                ChangeState(input.IsFalling ? player.fallingState : player.idleState);
                return;
            }
        }


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
