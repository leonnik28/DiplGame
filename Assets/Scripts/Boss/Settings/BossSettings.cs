using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Boss", menuName = "Boss/Create New Boss Settings")]
public class BossSettings : ScriptableObject
{
    public AttackSettings MeleeAttackSettings { get => _meleeAttackSettings; set => _meleeAttackSettings = value; }
    public AttackSettings RangedAttackSettings { get => _rangedAttackSettings; set => _rangedAttackSettings = value; }
    public UnityEngine.GameObject CatapultObject { get => _catapultObject; set => _catapultObject = value; }
    public UnityEngine.GameObject DirectBulletObject { get => _directBulletObject; set => _directBulletObject = value; }
    public float MoveSpeed { get => _moveSpeed; set => _moveSpeed = value; }
    public float RotationSpeed { get => _rotationSpeed; set => _rotationSpeed = value; }
    public int Health { get => _health; set => _health = value; }
    public float ProjectileSpeed { get => _projectileSpeed; set => _projectileSpeed = value; }
    public BossMovementSettings MovementSettings { get => _movementSettings; set => _movementSettings = value; }
    public List<BaseResourceSettings> Resources { get => _resources; set => _resources = value; }
    public List<UnityEngine.GameObject> SpawnedMobs { get => _spawnedMobs; set => _spawnedMobs = value; }
    public AttackSettings SpawnAttackSettings { get => _spawnAttackSettings; set => _spawnAttackSettings = value; }

    [SerializeField] private AttackSettings _meleeAttackSettings;
    [SerializeField] private AttackSettings _rangedAttackSettings;
    [SerializeField] private AttackSettings _spawnAttackSettings;

    [SerializeField] private UnityEngine.GameObject _catapultObject;
    [SerializeField] private UnityEngine.GameObject _directBulletObject;

    [SerializeField] private BossMovementSettings _movementSettings;
    [SerializeField] private List<BaseResourceSettings> _resources;

    [SerializeField] private List<UnityEngine.GameObject> _spawnedMobs;


    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotationSpeed;

    [SerializeField] private int _health;
    [SerializeField] private float _projectileSpeed;
}
