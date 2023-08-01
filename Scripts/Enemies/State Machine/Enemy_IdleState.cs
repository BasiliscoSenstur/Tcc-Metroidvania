using Unity.VisualScripting;
using UnityEngine;

public class Enemy_IdleState : Enemy_Abstract
{
    float turnSpeed = 5f;
    public override void EnterState(EnemyController enemy)
    {

    }
    public override void UpdateLogics(EnemyController enemy)
    {
        if (enemy.data.flyer)
        {
            if(Vector3.Distance(enemy.transform.position, enemy.startPos) < 0.1f)
            {
                enemy.data.ChangeEnemyAnimation(enemy.anim, "Idle");
                enemy.rb.velocity = Vector2.zero;
            }

            enemy.transform.position = Vector3.MoveTowards
                (enemy.transform.position, enemy.startPos, enemy.data.speed * Time.fixedDeltaTime);

            enemy.transform.rotation = Quaternion.Lerp
                (enemy.transform.rotation, enemy.startRot, turnSpeed * Time.fixedDeltaTime);

            if (enemy.chase)
            {
                enemy.SwitchState(enemy.attackState);
            }
        }
    }
    public override void UpdatePhysics(EnemyController enemy)
    {

    }
    public override void ExitState(EnemyController enemy)
    {

    }
}
