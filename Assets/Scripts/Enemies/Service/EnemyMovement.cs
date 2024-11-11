using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public Action OnAttack;

    [SerializeField] private Animator _animator;
    [SerializeField] private EnemyMovementSettings _settings;

    private Vector3 _initialPosition;

    private NavMeshAgent _agent;

    private Player _player;
    private Transform _playerTransform;

    private bool _isAttacking = false;
    private bool _isReturning = false;
    private bool _isPlayerDetected = false;

    private CancellationTokenSource _cancellationTokenSource;

    private void Awake()
    {
        _agent = GetComponentInParent<NavMeshAgent>();
        _initialPosition = transform.position;
        _player = FindObjectOfType<Player>();
        _playerTransform = _player.transform;
        _cancellationTokenSource = new CancellationTokenSource();
    }

    private async void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInChildren<Player>())
        {
            _isPlayerDetected = true;
            await CheckPlayerPosition(_cancellationTokenSource.Token);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponentInChildren<Player>())
        {
            _isPlayerDetected = false;
            _isAttacking = false;
            _isReturning = false;
            _agent.isStopped = false;
            _animator.SetInteger("animations", 0);
        }
    }

    public void StopMovement()
    {
        _cancellationTokenSource.Cancel();
        _cancellationTokenSource.Dispose();

        _agent.isStopped = true;
        _agent.enabled = false;
        GetComponent<Collider>().enabled = false;
        this.enabled = false;
    }

    private async UniTask CheckPlayerPosition(CancellationToken cancellationToken)
    {
        while (_isPlayerDetected)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                break;
            }

            float distanceToPlayer = Vector3.Distance(transform.position, _playerTransform.position);

            if (distanceToPlayer <= _settings.AttackRadius && !_isAttacking)
            {
                OnAttack?.Invoke();
                _agent.isStopped = true;
                _isAttacking = true;
            }
            else if (distanceToPlayer <= _settings.AttackRadius && _isAttacking)
            {
                _isAttacking = false;
            }
            else if (distanceToPlayer > _settings.ReturnToPositionRadius && !_isReturning)
            {
                _animator.SetInteger("animations", 2);
                _agent.SetDestination(_initialPosition);
                _isReturning = true;
            }
            else if (_isReturning && Vector3.Distance(transform.position, _initialPosition) <= 0.1f)
            {
                _animator.SetInteger("animations", 0);
                _isReturning = false;
            }
            else if (distanceToPlayer <= _settings.DetectionRadius && !_isAttacking && !_isReturning)
            {
                _animator.SetInteger("animations", 2);
                _agent.isStopped = false;
                _agent.SetDestination(_playerTransform.position);
            }
            else if (_isAttacking && distanceToPlayer > _settings.AttackRadius)
            {
                _animator.SetInteger("animations", 2);
                _isAttacking = false;
                _agent.isStopped = false;
            }

            if (_isAttacking)
            {
                Vector3 direction = (_playerTransform.position - _animator.transform.position);
                direction.y = 0; // Ignore the y component
                direction = direction.normalized;
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                _animator.transform.rotation = Quaternion.Slerp(_animator.transform.rotation, lookRotation, Time.deltaTime * _settings.RotationSpeed);
            }

            await UniTask.Delay(TimeSpan.FromSeconds((int)_settings.CheckInterval));
        }
    }

    private void OnDestroy()
    {
        if (!_cancellationTokenSource.IsCancellationRequested)
        {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
        }
    }
}
