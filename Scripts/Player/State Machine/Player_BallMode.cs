using System.ComponentModel;
using UnityEngine;

public class Player_BallMode : Abstract
{
    float changeModeCounter;
    bool canDoubleJump;

    public override void EnterState(PlayerController player)
    {
        player.ChangeMode();
        changeModeCounter = 0.5f;

    }
    public override void LogicsUpdate(PlayerController player)
    {
        //Jump e Double Jump
        if (player.isGrounded)
        {
            canDoubleJump = true;

            if (Input.GetButtonDown("Jump"))
            {
                player.Jump();
            }
        }
        else
        {
            if (Input.GetButtonDown("Jump") && canDoubleJump)
            {
                player.Jump();
                canDoubleJump = false;
            }
        }

        //Bomb
        if (player.canDropBomb)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                player.Shot(null);
            }
        }

        //Animations
        if (player.xAxis != 0)
        {
            player.ChangeAnimationState("Player_Ball_Move");
        }
        else
        {
            player.ChangeAnimationState("Player_Ball_Idle");
        }

        //Change Mode
        if (player.yAxis == 1)
        {
            changeModeCounter -= Time.deltaTime;
            if (changeModeCounter <= 0)
            {
                changeModeCounter = 0;
                player.ChangeMode();
                player.SwitchState(player.idleState);
            }
        }
        else
        {
            changeModeCounter = 0.5f;
        }
    }
    public override void PhysicsUpdate(PlayerController player)
    {
        player.rb.velocity = new Vector2(player.xAxis * player.moveSpeed, player.rb.velocity.y);
    }
    public override void ExitState(PlayerController player)
    {

    }
}
