using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class Player : MonoBehaviour
{
    public int CountBee => _countBee;

    [SerializeField] private Movement _movement;
    [SerializeField] private Animator _animator;
    [SerializeField] private AttackSettings _attackSettings;

    private readonly int _countBee = 0;

    private Vector2 _inputVector;

    private PlayerAttack _attack;

    private GameSession _gameSession;

    [Inject]
    private void Construct(GameSession gameSession)
    {
        _gameSession = gameSession;
    }

    private void OnEnable()
    {
        _gameSession.OnDataChange += UpdateData;
        _attack = new PlayerAttack(_attackSettings, _animator);
    }

    private void Update()
    {
        _movement.Move(_inputVector);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _attack.Attack(transform);
        }
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
