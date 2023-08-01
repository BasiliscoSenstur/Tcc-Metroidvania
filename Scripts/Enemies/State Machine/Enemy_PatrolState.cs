using Unity.VisualScripting;
using UnityEngine;

public class Enemy_PatrolState : Enemy_Abstract
{
    int currentPoint;
    float waitCounter;
    public override void EnterState(EnemyController enemy)
    {
        foreach (Transform point in enemy.patrolPoints)
        {
            point.SetParent(null);
        }
    }
    public override void UpdateLogics(EnemyController enemy)
    {
        if(Mathf.Abs(enemy.transform.position.x - enemy.patrolPoints[currentPoint].position.x) > 0.2f)
        {
            if(enemy.transform.position.x < enemy.patrolPoints[currentPoint].position.x)
            {
                enemy.transform.localScale = new Vector3(-1f, 1f, 1f);
                enemy.rb.velocity = new Vector2(enemy.data.speed, enemy.rb.velocity.y);
            }
            else
            {
                enemy.transform.localScale = Vector3.one;
                enemy.rb.velocity = new Vector2(-enemy.data.speed, enemy.rb.velocity.y);
            }
        }
        else
        {
            enemy.rb.velocity = new Vector2(0f, enemy.rb.velocity.y);

            waitCounter -= Time.deltaTime;

            if (waitCounter <= 0)
            {
                waitCounter = Random.Range(1.08f, 2.16f);

                currentPoint++;

                if (currentPoint >= enemy.patrolPoints.Length)
                {
                    currentPoint = 0;
                }
            }

            if (enemy.transform.position.y < enemy.patrolPoints[currentPoint].transform.position.y - 0.5f 
                && enemy.rb.velocity.y < 0.1f)
            {
                enemy.rb.velocity = new Vector2(enemy.rb.velocity.x, enemy.data.jumpForce);
            }
        }

        //Animations
        if (enemy.rb.velocity.x != 0)
        {
            enemy.data.ChangeEnemyAnimation(enemy.anim, "Move");
        }
        else
        {
            enemy.data.ChangeEnemyAnimation(enemy.anim, "Idle");
        }
    }
    public override void UpdatePhysics(EnemyController enemy)
    {

    }
    public override void ExitState(EnemyController enemy)
    {

    }
}
