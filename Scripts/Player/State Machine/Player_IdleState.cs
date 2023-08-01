using Unity.VisualScripting;
using UnityEngine;

public class Player_IdleState : Abstract
{
    float changeModeCounter;
    public override void EnterState(PlayerController player)
    {
        changeModeCounter = 0.5f;
        player.ChangeAnimationState("Player_Idle");
    }
    public override void LogicsUpdate(PlayerController player)
    {
        if (!player.noInput)
        {
            //Jump
            if (Input.GetButtonDown("Jump") && player.coyoteCounter > 0)
            {
                player.Jump();
            }
            if (player.rb.velocity.y > 0.1f)
            {
                player.SwitchState(player.jumpState);
            }

            //Fire
            if (Input.GetButtonDown("Fire1"))
            {
                player.SwitchState(player.shootState);
            }

            //Dash
            if (player.canDash)
            {
                if (Input.GetButtonDown("Fire2"))
                {
                    player.SwitchState(player.dashState);
                }
            }

            //Change Mode
            if (player.canChangeMode)
            {
                if (player.yAxis == -1)
                {
                    changeModeCounter -= Time.deltaTime;
                    if (changeModeCounter <= 0)
                    {
                        changeModeCounter = 0;
                        player.SwitchState(player.ballState);
                    }
                }
                else
                {
                    changeModeCounter = 0.5f;
                }
            }
        }

        //Run
        if (player.xAxis != 0)
        {
            player.SwitchState(player.runState);
        }

        //Shoot Up
        if(player.yAxis == 1) 
        {
            player.SwitchState(player.shootUpState);
        }

        //Controla movimentos inesperados
        if (player.xAxis == 0 && player.rb.velocity.x != 0)
        {
            player.rb.velocity = new Vector2(0f, player.rb.velocity.y);
        }
    }
    public override void PhysicsUpdate(PlayerController player)
    {
        
    }
    public override void ExitState(PlayerController player)
    {

    }
}
