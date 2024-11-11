using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class Player : MonoBehaviour, IDamageble
{
    public int CountBee => _countBee;

    [SerializeField] private Animator _animator;
    [SerializeField] private AttackSettings _attackSettings;
    [SerializeField] private PlayerMovementSettings _playerMovementSettings;
    [SerializeField] private int _health;

    private readonly int _countBee = 0;

    private Vector2 _inputVector;

    private PlayerAttack _attack;
    private PlayerMovement _movement;

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
        _movement = new PlayerMovement(GetComponent<Rigidbody>(), _animator, _playerMovementSettings);
    }

    private void Update()
    {
        _movement.Move(_inputVector, _animator.transform);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _attack.Attack(_animator.transform);
        }
    }

    private void OnDisable()
    {
        _gameSession.OnDataChange -= UpdateData;
    }

    public async void TakeDamage(int damage)
    {
        await DelayTakeDamage();
        _health -= damage;
        _animator.SetTrigger("takeDamage");
    }

    private async UniTask DelayTakeDamage()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(0.3));
    }

    private void UpdateData(UserData.SaveData data)
    {
    }

    private void OnMove(InputValue value)
    {
        _inputVector = value.Get<Vector2>();
    }
}
