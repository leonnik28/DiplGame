using UnityEngine;

[CreateAssetMenu(fileName = "New Player Movement", menuName = "Player/Create New Player Movement")]
public class PlayerMovementSettings : ScriptableObject
{
    public float MoveSpeed { get => _moveSpeed; set => _moveSpeed = value; }
    public float RotationSpeed { get => _rotationSpeed; set => _rotationSpeed = value; }

    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _rotationSpeed = 0.1f;
}
