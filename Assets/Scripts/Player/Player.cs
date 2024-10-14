using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class Player : MonoBehaviour
{
    public int CountBee => _countBee;

    [SerializeField] private Movement _movement;

    private int _countBee = 0;

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
        if (data.levelBee > 0 && _countBee != data.levelBee)
        {
            _countBee = data.levelBee;
        }
        else
        {
            _countBee = 1;
        }
    }

    private void OnMove(InputValue value)
    {
        _inputVector = value.Get<Vector2>();
    }
}
