using UnityEngine;

[CreateAssetMenu(fileName = "New Attack", menuName = "Attack/Create New Attack")]
public class AttackSettings : ScriptableObject
{
    public int Damage { get => _damage; set => _damage = value; }
    public float Range { get => _range; set => _range = value; }
    public float Angle { get => _angle; set => _angle = value; }
    public float CooldownTime { get => _cooldownTime; set => _cooldownTime = value; }

    [SerializeField] private int _damage;
    [SerializeField] private float _range;
    [SerializeField] private float _angle;
    [SerializeField] private float _cooldownTime;
}
