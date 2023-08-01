using UnityEngine;

public abstract class Enemy_Abstract
{
    public abstract void EnterState(EnemyController enemy);
    public abstract void UpdateLogics(EnemyController enemy);
    public abstract void UpdatePhysics(EnemyController enemy);
    public abstract void ExitState(EnemyController enemy);
}
