using UnityEngine;

public class MoveState : IBossState
{
    private float _rangedAttackCheckTimer = 0f;
    private const float RangedAttackCheckInterval = 2f;

    public void Enter(Boss boss)
    {
        boss.Movement.ChasePlayer();
    }

    public void Execute(Boss boss)
    {
        _rangedAttackCheckTimer += Time.deltaTime;

        if (boss.Movement.IsInMeleeAttackRange())
        {
            boss.StateMachine.TransitionToState(boss.StateMachine.MeleeAttackState);
        }
        else if (_rangedAttackCheckTimer >= RangedAttackCheckInterval)
        {
            boss.Movement.ChasePlayer();
            if (boss.Movement.IsInRangedAttackRange())
            {
                boss.StateMachine.TransitionToState(boss.StateMachine.RangeAttackState);
            }
            _rangedAttackCheckTimer = 0f;
        }
    }

    public void Exit(Boss boss)
    {
        boss.Movement.StopChasing();
    }
}
