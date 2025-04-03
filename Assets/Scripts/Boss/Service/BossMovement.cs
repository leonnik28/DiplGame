using System.Threading;
using UnityEngine.AI;
using UnityEngine;

public class BossMovement
{
    public bool IsInMeleeAttackRange() => Vector3.Distance(_animator.transform.position, _playerTransform.position) <= _settings.AttackRadius;
    public bool IsInRangedAttackRange() => Vector3.Distance(_animator.transform.position, _playerTransform.position) <= _settings.RangeAttackRadius;
    public bool IsInReturnRange() => Vector3.Distance(_animator.transform.position, _playerTransform.position) > _settings.ReturnToPositionRadius;
    public bool IsAtInitialPosition() => Vector3.Distance(_animator.transform.position, _initialPosition) <= 0.75f;
    public bool IsPlayerDetected { get; set; }

    private readonly Animator _animator;
    private readonly BossMovementSettings _settings;
    private readonly NavMeshAgent _agent;
    private readonly Player _player;

    private Vector3 _initialPosition;
    private Transform _playerTransform;
    
    private CancellationTokenSource _cancellationTokenSource;

    public BossMovement(Animator animator, BossMovementSettings settings, NavMeshAgent agent, Player player)
    {
        _animator = animator;
        _settings = settings;
        _agent = agent;
        _player = player;
    }

    public void Initialize()
    {
        _initialPosition = _animator.transform.position;
        _playerTransform = _player.transform;
        _cancellationTokenSource = new CancellationTokenSource();
    }

    public void ChasePlayer()
    {
        _animator.SetInteger("animations", 2);
        _agent.isStopped = false;
        _agent.SetDestination(_playerTransform.position);
    }

    public void StopChasing()
    {
        _animator.SetInteger("animations", 0);
        _agent.isStopped = true;
    }

    public void StartReturning()
    {
        _animator.SetInteger("animations", 2);
        _agent.SetDestination(_initialPosition);
    }

    public void StopReturning()
    {
        _animator.SetInteger("animations", 0);
    }

    public void DestroyMovement()
    {
        if (!_cancellationTokenSource.IsCancellationRequested)
        {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
        }

        _agent.isStopped = true;
        _agent.enabled = false;
    }

    public void RotateTowardsPlayer()
    {
        Vector3 direction = (_playerTransform.position - _animator.transform.position);
        direction.y = 0;
        direction = direction.normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        _animator.transform.rotation = Quaternion.Slerp(_animator.transform.rotation, lookRotation, Time.deltaTime * _settings.RotationSpeed);
    }
}
