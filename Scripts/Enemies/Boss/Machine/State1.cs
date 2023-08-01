using System.Collections;
using UnityEngine;

public class State1 : Boss_Abstract
{
    float activeTime;
    int changeFase;

    float fireRate;

    public override void EnterState(BossController boss)
    {
        //Randomize the trashold of the fase
        changeFase = Random.Range(boss.data.fase2 - 10, boss.data.fase2 + 10);

        activeTime = 5f;
        fireRate = 0.81f;

        boss.data.ChangeEnemyAnimation(boss.anim, "Phantom_Appears");
    }
    public override void UpdateLogics(BossController boss)
    {
        //Firebal
        if (fireRate > 0)
        {
            fireRate -= Time.deltaTime;
        }
        if (fireRate <= 0)
        {
            boss.ShotFireball();
            fireRate = 0.81f;
        }

        //Spawn
        if(activeTime > 0)
        {
            activeTime -= Time.deltaTime;
        }
        else
        {
            boss.StartChangePositionCo();
            activeTime = 5f;
        }

        //Fase2
        if (boss.currentBossHp < boss.data.maxHP - changeFase)
        {
            boss.SwitchState(boss.state2);
        }
    }
    public override void UpdatePhysics(BossController boss)
    {

    }
    public override void ExitState(BossController boss)
    {

    }
}
