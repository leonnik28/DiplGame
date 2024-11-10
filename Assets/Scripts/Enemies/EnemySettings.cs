using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy/Create New Enemy")]
public class EnemySettings : ScriptableObject
{
    public AttackSettings AttackSettings { get => _attackSettings; set => _attackSettings = value; }
    public int Health { get => _health; set => _health = value; }
    public List<BaseResourceSettings> Resources { get => _resources; set => _resources = value; }

    [SerializeField] private AttackSettings _attackSettings;
    [SerializeField] private int _health;
    [SerializeField] private List<BaseResourceSettings> _resources;
}
