using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement
{
    public Action OnAttack;

    private readonly Animator _animator;
    private readonly EnemyMovementSettings _settings;
    private readonly NavMeshAgent _agent;
    private readonly Player _player;

    private Vector3 _initialPosition;
    private Transform _playerTransform;

    private bool _isAttacking = false;
    private bool _isReturning = false;
    private bool _isPlayerDetected = false;
    private bool _isPlay = false;

    private CancellationTokenSource _cancellationTokenSource;

    public EnemyMovement(Animator animator, EnemyMovementSettings settings, NavMeshAgent agent, Player player)
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

    public async UniTaskVoid StartMove()
    {
        _isPlayerDetected = true;
        if (!_isPlay)
        {
            _isPlay = true;
            await CheckPlayerPosition(_cancellationTokenSource.Token);
        }
    }

    public void ReturnToInitialPosition()
    {
        StartReturning();
        _isPlayerDetected = false;
        _isPlay = false;
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

    private async UniTask CheckPlayerPosition(CancellationToken cancellationToken)
    {
        while (_isPlayerDetected)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                break;
            }

            float distanceToPlayer = Vector3.Distance(_animator.transform.position, _playerTransform.position);

            if (distanceToPlayer <= _settings.AttackRadius && !_isAttacking)
            {
                StartAttack();
            }
            else if (distanceToPlayer <= _settings.AttackRadius && _isAttacking)
            {
                StopAttack();
            }
            else if (distanceToPlayer > _settings.ReturnToPositionRadius && !_isReturning)
            {
                StartReturning();
            }
            else if (_isReturning && Vector3.Distance(_animator.transform.position, _initialPosition) <= 0.75f)
            {
                StopReturning();
            }
            else if (distanceToPlayer <= _settings.DetectionRadius && !_isAttacking && !_isReturning)
            {
                ChasePlayer();
            }
            else if (_isAttacking && distanceToPlayer > _settings.AttackRadius)
            {
                ChasePlayer();
            }

            if (_isAttacking)
            {
                RotateTowardsPlayer();
            }

            await UniTask.Delay(TimeSpan.FromSeconds((int)_settings.CheckInterval));
        }
    }

    private void StartAttack()
    {
        _animator.SetInteger("animations", 0);
        OnAttack?.Invoke();
        _agent.isStopped = true;
        _isAttacking = true;
    }

    private void StopAttack()
    {
        _isAttacking = false;
    }

    private void StartReturning()
    {
        _animator.SetInteger("animations", 2);
        _agent.SetDestination(_initialPosition);
        _isReturning = true;
    }

    private void StopReturning()
    {
        _animator.SetInteger("animations", 0);
        _isReturning = false;
    }

    private void ChasePlayer()
    {
        _animator.SetInteger("animations", 2);
        _agent.isStopped = false;
        _agent.SetDestination(_playerTransform.position);
        _isAttacking = false;
    }

    private void RotateTowardsPlayer()
    {
        Vector3 direction = (_playerTransform.position - _animator.transform.position);
        direction.y = 0;
        direction = direction.normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        _animator.transform.rotation = Quaternion.Slerp(_animator.transform.rotation, lookRotation, Time.deltaTime * _settings.RotationSpeed);
    }
}
