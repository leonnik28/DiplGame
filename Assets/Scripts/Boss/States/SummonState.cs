public class SummonState : IBossState
{
    public void Enter(Boss boss)
    {
        if (boss.SpawnAttack.IsCooldown())
        {
            boss.StateMachine.TransitionToState(boss.StateMachine.IdleState);
        }
        else
        {
            boss.SpawnAttack.SpawnAttack();
        }
    }

    public void Execute(Boss boss)
    {
        if (boss.IsEndAttack)
        {
            boss.IsEndAttack = false;
            boss.StateMachine.TransitionToState(boss.StateMachine.IdleState);
        }
    }

    public void Exit(Boss boss)
    {
    }
}