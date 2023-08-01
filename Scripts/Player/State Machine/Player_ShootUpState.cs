using System.Threading;
using UnityEngine;

public class Player_ShootUpState : Abstract
{
    public override void EnterState(PlayerController player)
    {
        player.ChangeAnimationState("Player_Shoot_Up");
    }
    public override void LogicsUpdate(PlayerController player)
    {
        //Idle
        if (player.yAxis != 1 && player.xAxis == 0)
        {
            player.SwitchState(player.idleState);
        }

        //UpShot
        if(player.yAxis == 1)
        {
            player.SwitchState(player.shootUpState);
        }
        if (!player.noInput)
        {
            //Shot
            if (Input.GetButtonDown("Fire1"))
            {
                player.Shot(player.upFirePoint);
            }

            //Jump
            if (Input.GetButtonDown("Jump") && player.coyoteCounter > 0)
            {
                player.Jump();
            }
            if (player.rb.velocity.y > 0.1f)
            {
                player.SwitchState(player.jumpState);
            }
        }

        //Run
        if (player.xAxis != 0)
        {
            player.SwitchState(player.runState);
        }
    }
    public override void PhysicsUpdate(PlayerController player)
    {

    }
    public override void ExitState(PlayerController player)
    {

    }
}
