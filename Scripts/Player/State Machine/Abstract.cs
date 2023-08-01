using UnityEngine;

public abstract class Abstract
{
    public abstract void EnterState(PlayerController player);
    public abstract void LogicsUpdate(PlayerController player);
    public abstract void PhysicsUpdate(PlayerController player);
    public abstract void ExitState(PlayerController player);

}
