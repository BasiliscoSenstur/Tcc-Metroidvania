using UnityEngine;

public class Enemy_AttackState : Enemy_Abstract
{
    float turnSpeed = 5f;
    public override void EnterState(EnemyController enemy)
    {
        enemy.data.ChangeEnemyAnimation(enemy.anim, "Move");
    }
    public override void UpdateLogics(EnemyController enemy)
    {
        if (enemy.data.flyer)
        {
            Vector3 direction = enemy.transform.position - enemy.attackTarget.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            enemy.transform.rotation = Quaternion.Lerp(enemy.transform.rotation, rotation, turnSpeed * Time.fixedDeltaTime);
        }
        if (!enemy.chase)
        {
            enemy.SwitchState(enemy.idleState); 
        }
    }
    public override void UpdatePhysics(EnemyController enemy)
    {   
        //Gambiarra! BUG atravessa Colider do player
        float amount = 0.66f;
        Vector2 target = new Vector2(enemy.attackTarget.position.x - amount, enemy.attackTarget.position.y - amount);
        //
        enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, target,
            enemy.data.attackSpeed * Time.fixedDeltaTime);
    }
    public override void ExitState(EnemyController enemy)
    {

    }
}
