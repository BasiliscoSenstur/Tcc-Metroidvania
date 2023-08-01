using UnityEngine;

public class Player_JumpState : Abstract
{
    bool stateCanDoubleJump;
    public override void EnterState(PlayerController player)
    {
        stateCanDoubleJump = true;

    }
    public override void LogicsUpdate(PlayerController player)
    {
        //Animations
        if (stateCanDoubleJump)
        {
            player.ChangeAnimationState("Player_Jump");
        }
        else
        {
            player.ChangeAnimationState("Player_Double_Jump");
        }

        if (!player.noInput)
        {
            //Shot
            if (Input.GetButtonDown("Fire1") && stateCanDoubleJump)
            {
                player.Shot(player.StandFirePoint);
            }

            //Double Jump
            if (player.canDoubleJump)
            {
                if (Input.GetButtonDown("Jump") && stateCanDoubleJump)
                {
                    player.Jump();

                    stateCanDoubleJump = false;
                }
            }
        }

        //Idle
        if (player.isGrounded)
        {
            player.rb.velocity = new Vector2(0f, player.rb.velocity.y);
            player.SwitchState(player.idleState);
        }

        //Jump Cut
        if (Input.GetButtonUp("Jump"))
        {
            player.rb.velocity = new Vector2(player.rb.velocity.x, player.rb.velocity.y * 0.4f);
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
