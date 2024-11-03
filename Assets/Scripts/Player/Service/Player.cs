using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class Player : MonoBehaviour
{
    public int CountBee => _countBee;

    [SerializeField] private Movement _movement;

    private readonly int _countBee = 0;

    private Vector2 _inputVector;

    private GameSession _gameSession;

    [Inject]
    private void Construct(GameSession gameSession)
    {
        _gameSession = gameSession;
    }

    private void OnEnable()
    {
        _gameSession.OnDataChange += UpdateData;
    }

    private void Update()
    {
        _movement.Move(_inputVector);
    }

    private void OnDisable()
    {
        _gameSession.OnDataChange -= UpdateData;
    }

    private void UpdateData(UserData.SaveData data)
    {
    }

    private void OnMove(InputValue value)
    {
        _inputVector = value.Get<Vector2>();
    }
}
