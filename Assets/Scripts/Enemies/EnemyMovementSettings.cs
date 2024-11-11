using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy/Create New Enemy Movement")]
public class EnemyMovementSettings : ScriptableObject
{
    public float DetectionRadius { get => _detectionRadius; set => _detectionRadius = value; }
    public float AttackRadius { get => _attackRadius; set => _attackRadius = value; }
    public float ReturnToPositionRadius { get => _returnToPositionRadius; set => _returnToPositionRadius = value; }
    public float CheckInterval { get => _checkInterval; set => _checkInterval = value; }

    [SerializeField] private float _detectionRadius;
    [SerializeField] private float _attackRadius;
    [SerializeField] private float _returnToPositionRadius;
    [SerializeField] private float _checkInterval;
}
