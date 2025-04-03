public class IdleState : IBossState
{
    public void Enter(Boss boss)
    {
    }

    public void Execute(Boss boss)
    {
        if (boss.Movement.IsInMeleeAttackRange())
        {
            float randomValue = UnityEngine.Random.value;

            if (randomValue < 0.5f)
            {
                boss.StateMachine.TransitionToState(boss.StateMachine.RangeAttackState);
            }
            else
            {
                boss.StateMachine.TransitionToState(boss.StateMachine.MeleeAttackState);
            }
        }
        else if (boss.Movement.IsPlayerDetected)
        {
            boss.StateMachine.TransitionToState(boss.StateMachine.MoveState);
        }
    }

    public void Exit(Boss boss)
    {
    }
}