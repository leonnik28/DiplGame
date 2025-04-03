public class MeleeAttackState : IBossState
{
    public void Enter(Boss boss)
    {
        float randomValue = UnityEngine.Random.value;

        if (boss.MeleeAttack.IsCooldown())
        {
            boss.StateMachine.TransitionToState(boss.StateMachine.IdleState);
        }
        else if (randomValue < 0.5f)
        {
            boss.MeleeAttack.Attack(boss.transform);
        }
        else
        {
            boss.MeleeAttack.ChargedAttack(boss.transform);
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