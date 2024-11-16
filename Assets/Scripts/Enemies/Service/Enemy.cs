using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class Enemy : MonoBehaviour, IDamageble
{
    [SerializeField] private EnemySettings _settings;
    [SerializeField] private Animator _animator;
    [SerializeField] private Collider _triggerCollider;

    private EnemyAttack _attack;
    private EnemyMovement _movement;

    private Player _player;

    private int _currentHealth;
    private ResourcePresenter _presenter;

    [Inject]
    private void Construct(ResourcePresenter presenter, Player player)
    {
        _presenter = presenter;
        _player = player;
    }

    private void Awake()
    {
        _currentHealth = _settings.Health;

        _attack = new EnemyAttack(_settings.AttackSettings, _animator);
        _movement = new EnemyMovement(_animator, _settings.MovementSettings, GetComponent<NavMeshAgent>(), _player);
        _movement.Initialize();
        _movement.OnAttack += Attack;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            _attack.Attack(transform);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInChildren<Player>())
        {
            _ = _movement.StartMove();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponentInChildren<Player>())
        {
            _movement.ReturnToInitialPosition();
        }
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        _animator.SetTrigger("takeDamage");
        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    private void Attack()
    {
        _attack.Attack(transform);
    }

    private void Die()
    {
        _movement.DestroyMovement();
        _attack.StopAttack();
        _animator.SetTrigger("die");
        Collider collider = transform.GetComponent<Collider>();
        collider.enabled = false;
        _triggerCollider.enabled = false;

        Vector3 spawnPosition = transform.position;
        spawnPosition.y += 1;
        foreach (BaseResourceSettings resource in _settings.Resources)
        {
            _presenter.SpawnItem(spawnPosition, resource);
        }
        _movement = null;
        _attack = null;
    }
}
