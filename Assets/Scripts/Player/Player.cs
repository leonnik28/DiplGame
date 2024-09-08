using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class Player : MonoBehaviour
{
    public int CountBee => _countBee;

    [SerializeField] private Movement _movement;

    private int _countBee = 1;

    private Vector2 _inputVector;

    private void Update()
    {
        _movement.Move(_inputVector);
    }

    private void OnMove(InputValue value)
    {
        _inputVector = value.Get<Vector2>();
    }
}
