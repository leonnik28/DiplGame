using Cysharp.Threading.Tasks;
using System.Threading;
using System;
using UnityEngine.AI;
using UnityEngine;

public class BossMovement
{
    public Action OnAttack;
    public Action OnSummon;
    public Action OnLifeDrain;
    public Action OnChargedAttack;

    private readonly Animator _animator;
    private readonly BossMovementSettings _settings;
    private readonly NavMeshAgent _agent;
    private readonly Player _player;

    private Vector3 _initialPosition;
    private Transform _playerTransform;

    private bool _isAttacking = false;
    private bool _isReturning = false;
    private bool _isPlayerDetected = false;
    private bool _isPlay = false;
    private bool _isSummoning = false;
    private bool _isLifeDraining = false;
    private bool _isChargedAttacking = false;
    private int _stage = 1;

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

            if (distanceToPlayer <= GetAttackRadius() && !_isAttacking && !_isSummoning && !_isLifeDraining && !_isChargedAttacking)
            {
                StartAttack();
            }
            else if (distanceToPlayer <= _settings.SummonRadius && !_isSummoning && !_isAttacking && !_isLifeDraining && !_isChargedAttacking)
            {
                if (UnityEngine.Random.value < _settings.SummonProbability)
                {
                    StartSummon();
                }
            }
            else if (distanceToPlayer <= _settings.LifeDrainRadius && !_isLifeDraining && !_isAttacking && !_isSummoning && !_isChargedAttacking)
            {
                if (UnityEngine.Random.value < _settings.LifeDrainProbability)
                {
                    StartLifeDrain();
                }
            }
            else if (distanceToPlayer > _settings.ReturnToPositionRadius && !_isReturning)
            {
                StartReturning();
            }
            else if (_isReturning && Vector3.Distance(_animator.transform.position, _initialPosition) <= 0.75f)
            {
                StopReturning();
            }
            else if (distanceToPlayer <= _settings.DetectionRadius && !_isAttacking && !_isReturning && !_isSummoning && !_isLifeDraining && !_isChargedAttacking)
            {
                ChasePlayer();
            }
            else if (_isAttacking && distanceToPlayer > GetAttackRadius())
            {
                ChasePlayer();
            }

            if (_isAttacking || _isSummoning || _isLifeDraining || _isChargedAttacking)
            {
                RotateTowardsPlayer();
            }

            await UniTask.Delay(TimeSpan.FromSeconds((int)_settings.CheckInterval));
        }
    }

    private float GetAttackRadius()
    {
        return _settings.AttackRadius;
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

    private void StartSummon()
    {
        _animator.SetInteger("animations", 1);
        OnSummon?.Invoke();
        _agent.isStopped = true;
        _isSummoning = true;
    }

    private void StopSummon()
    {
        _isSummoning = false;
    }

    private void StartLifeDrain()
    {
        _animator.SetInteger("animations", 3);
        OnLifeDrain?.Invoke();
        _agent.isStopped = true;
        _isLifeDraining = true;
    }

    private void StopLifeDrain()
    {
        _isLifeDraining = false;
    }

    private void StartChargedAttack()
    {
        _animator.SetInteger("animations", 4);
        OnChargedAttack?.Invoke();
        _agent.isStopped = true;
        _isChargedAttacking = true;
    }

    private void StopChargedAttack()
    {
        _isChargedAttacking = false;
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

    private void Dodge()
    {
        Vector3 dodgeDirection = UnityEngine.Random.insideUnitCircle.normalized;
        _agent.SetDestination(_animator.transform.position + new Vector3(dodgeDirection.x, 0, dodgeDirection.y) * _settings.DodgeDistance);
    }

    public void IncreaseStage()
    {
        _stage++;
        if (_stage >= 2)
        {
            // Implement any additional logic for stage 2 and 3
        }
    }
}