using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Boss Movement", menuName = "Boss/Create New Boss Movement Settings")]
public class BossMovementSettings : EnemyMovementSettings
{
    [SerializeField] private int _summonRadius;
    [SerializeField] private int _lifeDrainRadius;
    [SerializeField] private float _dodgeDistance;
    [SerializeField] private int _summonProbability;
    [SerializeField] private int _lifeDrainProbability;

    public int SummonRadius { get => _summonRadius; set => _summonRadius = value; }
    public int LifeDrainRadius { get => _lifeDrainRadius; set => _lifeDrainRadius = value; }
    public float DodgeDistance { get => _dodgeDistance; set => _dodgeDistance = value; }
    public int SummonProbability { get => _summonProbability; set => _summonProbability = value; }
    public int LifeDrainProbability { get => _lifeDrainProbability; set => _lifeDrainProbability = value; }
}
