using UnityEngine;
using Zenject;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemySettings _settings;
    [SerializeField] private Animator _animator;

    private EnemyAttack _attack;

    private int _currentHealth;
    private ResourcePresenter _presenter;

    [Inject]
    private void Construct(ResourcePresenter presenter)
    {
        _presenter = presenter;
        _currentHealth = _settings.Health;
        _attack = new EnemyAttack(_settings.AttackSettings, _animator);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            _attack.Attack(transform);
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

    private void Die()
    {
        _animator.SetTrigger("die");

        Vector3 spawnPosition = transform.position;
        spawnPosition.y += 1;
        foreach (BaseResourceSettings resource in _settings.Resources)
        {
            _presenter.SpawnItem(spawnPosition, resource);
        }
    }
}
