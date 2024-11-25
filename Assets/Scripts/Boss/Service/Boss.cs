using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class Boss : MonoBehaviour
{
    [SerializeField] private BossSettings _settings;
    [SerializeField] private Animator _animator;

    private BossMeleeAttack _meleeAttack;
    private BossRangedAttack _rangedAttack;

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

        _meleeAttack = new BossMeleeAttack(_settings.MeleeAttackSettings, _animator);
        _rangedAttack = new BossRangedAttack(_settings.RangedAttackSettings, _animator, transform, _settings.ProjectileSpeed);
    }
}
