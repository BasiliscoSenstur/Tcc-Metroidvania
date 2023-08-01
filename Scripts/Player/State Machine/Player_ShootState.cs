using UnityEngine;

public class Player_ShootState : Abstract
{
    float animationCounter;
    public override void EnterState(PlayerController player)
    {
        StateShot(player);
    }
    public override void LogicsUpdate(PlayerController player)
    {
        //Run
        if(player.xAxis != 0)
        {
            player.SwitchState(player.runState);
        }

        if (!player.noInput)
        {
            //Shot
            if (Input.GetButtonDown("Fire1"))
            {
                StateShot(player);
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

        //Shoot Up
        if (player.yAxis == 1)
        {
            player.SwitchState(player.shootUpState);
        }

        //Animation timer
        if (animationCounter > 0)
        {
            animationCounter -= Time.deltaTime;
        }
        if (animationCounter <= 0)
        {
            animationCounter = 0;
            player.SwitchState(player.idleState);
        }
    }
    public override void PhysicsUpdate(PlayerController player)
    {

    }
    public override void ExitState(PlayerController player)
    {

    }

    void StateShot(PlayerController player)
    {
        player.Shot(player.StandFirePoint);
        player.ChangeAnimationState("Player_Shoot_Stand");
        animationCounter = 0.2f;
    }
}
