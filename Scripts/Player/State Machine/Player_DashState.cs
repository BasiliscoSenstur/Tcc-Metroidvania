using UnityEngine;

public class Player_DashState : Abstract
{
    float dashSpeed = 25f;
    float dashCounter;
    public override void EnterState(PlayerController player)
    {
        dashCounter = 0.33f;

        player.DashTrail();
    }
    public override void LogicsUpdate(PlayerController player)
    {

        if (player.dashTrailCounter > 0)
        {
            player.dashTrailCounter -= Time.deltaTime;
        }
        if (player.dashTrailCounter <= 0)
        {
            player.DashTrail();
        }

        if (dashCounter > 0)
        {
            dashCounter -= Time.deltaTime;
        }
        if(dashCounter <= 0)
        {
            dashCounter = 0;

            if (player.isGrounded)
            {
                if (player.xAxis != 0)
                {
                    player.SwitchState(player.runState);
                }
                else
                {
                    player.SwitchState(player.idleState);
                }
            }
            else
            {
                player.SwitchState(player.jumpState);
            }
        }
    }
    public override void PhysicsUpdate(PlayerController player)
    {
        player.rb.velocity = new Vector2(dashSpeed * player.transform.localScale.x, player.rb.velocity.y);
    }
    public override void ExitState(PlayerController player)
    {

    }
}
