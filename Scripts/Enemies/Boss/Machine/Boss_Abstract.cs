using UnityEngine;

public abstract class Boss_Abstract
{
    public abstract void EnterState(BossController boss);
    public abstract void UpdateLogics(BossController boss);
    public abstract void UpdatePhysics(BossController boss);
    public abstract void ExitState(BossController boss);
}
