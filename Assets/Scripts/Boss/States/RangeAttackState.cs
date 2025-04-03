public class RangeAttackState : IBossState
{
    public void Enter(Boss boss)
    {
        float randomValue = UnityEngine.Random.value;

        if (randomValue < 0.1f)
        {
            if (boss.RangedAttack.IsCooldown())
            {
                boss.StateMachine.TransitionToState(boss.StateMachine.IdleState);
            }
            else
            {
                boss.RangedAttack.Attack(boss.Player.transform);
            }
        }
        else if (randomValue < 0.2f)
        {
            if (boss.RangedAttack.IsCooldown())
            {
                boss.StateMachine.TransitionToState(boss.StateMachine.IdleState);
            }
            else
            {
                boss.StateMachine.TransitionToState(boss.StateMachine.SummonState);
            }
        }
        else
        {
            boss.StateMachine.TransitionToState(boss.StateMachine.IdleState);
        }
    }

    public void Execute(Boss boss)
    {
        boss.Movement.RotateTowardsPlayer();
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