using UnityEngine;

public class State2 : Boss_Abstract
{
    float wait;
    float fireRate;
    public override void EnterState(BossController boss)
    {
        boss.StartChangePositionCo();
        wait = 1f;
        fireRate = 0.66f;
    }
    public override void UpdateLogics(BossController boss)
    {
        //Fireball
        if (fireRate > 0)
        {
            fireRate -= Time.deltaTime;
        }
        if (fireRate <= 0)
        {
            boss.ShotFireball();

            if (boss.currentBossHp < boss.data.maxHP - boss.data.fase3)
            {
                fireRate = 0.33f;
            }
            else
            {
                fireRate = 0.66f;
            }
        }

        //Position
        if (Vector3.Distance(boss.transform.position,boss.targetPoint.position) > 0.2f)
        {
            if (wait > 0)
            {
                wait -= Time.deltaTime;
            }
            if(wait <= 0)
            {
                wait = 0;
                boss.transform.position = Vector3.MoveTowards
                    (boss.transform.position, boss.targetPoint.position, boss.data.speed * Time.fixedDeltaTime);
            }
        }
        else
        {
            boss.StartChangePositionCo();
            boss.targetPoint = boss.spawnPoints[Random.Range(0, boss.spawnPoints.Length)];
            wait = 1f;
        }
    }
    public override void UpdatePhysics(BossController boss)
    {

    }
    public override void ExitState(BossController boss)
    {

    }
}