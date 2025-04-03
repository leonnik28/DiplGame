using UnityEngine.AI;
using UnityEngine;
using Zenject;

public class Boss : MonoBehaviour, IDamageble
{
    public bool IsEndAttack = false;

    public BossMeleeAttack MeleeAttack => _meleeAttack;
    public BossRangedAttack RangedAttack => _rangedAttack;
    public BossSpawnAttack SpawnAttack => _spawnAttack;
    public BossMovement Movement => _movement;
    public Player Player => _player;
    public BossStateMañhine StateMachine => _stateMachine;

    [SerializeField] private BossSettings _settings;
    [SerializeField] private Animator _animator;
    [SerializeField] private Collider _triggerCollider;

    private BossMeleeAttack _meleeAttack;
    private BossRangedAttack _rangedAttack;
    private BossSpawnAttack _spawnAttack;
    private BossMovement _movement;

    private Player _player;

    private int _currentHealth;
    private ResourcePresenter _presenter;

    private BossStateMañhine _stateMachine;

    private MobFactory _mobFactory;

    [Inject]
    private void Construct(ResourcePresenter presenter, Player player, MobFactory mobFactory)
    {
        _presenter = presenter;
        _player = player;
        _mobFactory = mobFactory;
    }

    private void Awake()
    {
        _currentHealth = _settings.Health;

        _meleeAttack = new BossMeleeAttack(_settings.MeleeAttackSettings, _animator);
        _rangedAttack = new BossRangedAttack(_settings.RangedAttackSettings, _animator, transform, _settings.ProjectileSpeed);
        _spawnAttack = new BossSpawnAttack(_settings.SpawnAttackSettings, _animator, _mobFactory, _settings.SpawnedMobs);
        _movement = new BossMovement(_animator, _settings.MovementSettings, GetComponent<NavMeshAgent>(), _player);

        _movement.Initialize();

        _stateMachine = new BossStateMañhine(this);
    }

    private void Update()
    {
        _stateMachine.Update();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInChildren<Player>())
        {
            _movement.IsPlayerDetected = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponentInChildren<Player>())
        {
            _movement.IsPlayerDetected = false;
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

    public void EndAttack()
    {
        IsEndAttack = true;
    }

    private void Die()
    {
        _movement.DestroyMovement();
        _meleeAttack.StopAttack();
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
        _meleeAttack = null;
    }
}
