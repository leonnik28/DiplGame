using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private float _detectionRadius = 10f;
    [SerializeField] private float _attackRadius = 2f;
    [SerializeField] private float _returnToPositionRadius = 150f;
    [SerializeField] private Transform _initialPosition;

    private NavMeshAgent _agent;

    private Player _player;
    private Transform _playerTransform;
    
    private bool _isAttacking = false;
    private bool _isReturning = false;

    private void Start()
    {
        _player = FindObjectOfType<Player>();
        _playerTransform = _player.transform;
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, _playerTransform.position);

        if (distanceToPlayer <= _detectionRadius && !_isAttacking && !_isReturning)
        {
            _animator.SetInteger("animations", 2); // Анимация движения
            _agent.SetDestination(_playerTransform.position);
        }
        else if (distanceToPlayer <= _attackRadius && !_isAttacking)
        {
            _animator.SetTrigger("attack"); // Анимация атаки
            _isAttacking = true;
            _agent.isStopped = true;
            AttackPlayer();
        }
        else if (distanceToPlayer > _returnToPositionRadius && !_isReturning)
        {
            _animator.SetInteger("animations", 2); // Анимация движения
            _agent.SetDestination(_initialPosition.position);
            _isReturning = true;
        }
        else if (_isReturning && Vector3.Distance(transform.position, _initialPosition.position) <= 0.1f)
        {
            _animator.SetInteger("animations", 0); // Анимация ожидания
            _isReturning = false;
        }
        else if (_isAttacking && distanceToPlayer > _attackRadius)
        {
            _animator.SetInteger("animations", 2); // Анимация движения
            _isAttacking = false;
            _agent.isStopped = false;
        }
    }

    private void AttackPlayer()
    {
    }
}
