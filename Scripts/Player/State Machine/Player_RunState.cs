using UnityEditor.Rendering;
using UnityEngine;

public class Player_RunState : Abstract
{
    float changeModeCounter;
    public override void EnterState(PlayerController player)
    {
        player.ChangeAnimationState("Player_Run");
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

            //Shot
            if (Input.GetButtonDown("Fire1"))
            {
                player.Shot(player.StandFirePoint);
            }

            //Dash
            if (player.canDash)
            {
                if (Input.GetButtonDown("Fire2"))
                {
                    player.SwitchState(player.dashState);
                }
            }
        }

        //Idle
        if (player.xAxis == 0)
        {
            player.rb.velocity = new Vector2(0f, player.rb.velocity.y);
            player.SwitchState(player.idleState);
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
    public override void PhysicsUpdate(PlayerController player)
    {
        player.rb.velocity = new Vector2(player.xAxis * player.moveSpeed, player.rb.velocity.y);
    }
    public override void ExitState(PlayerController player)
    {

    }
}
