using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    public int CountBee => _countBee;

    [SerializeField] private Movement _movement;
    [SerializeField] private Vector3 _spawnOffset;
    [SerializeField] private GameObject _beePrefab;

    private int _countBee = 0;
    private int _currentBeeCount = 0;

    private Vector2 _inputVector;

    private GameSession _gameSession;

    private List<Vector3> _beePositions = new List<Vector3>();

    [Inject]
    private void Construct(GameSession gameSession)
    {
        _gameSession = gameSession;
    }

    private void OnEnable()
    {
        _gameSession.OnDataChange += UpdateData;
    }

    private void OnDisable()
    {
        _gameSession.OnDataChange -= UpdateData;
    }

    private void UpdateData(UserData.SaveData data)
    {
        if (data.countBee > 0 && _countBee != data.countBee)
        {
            _countBee = data.countBee;
            SpawnBeesInTriangle();
        }
        else
        {
            _countBee = 1;
        }
    }

    private void SpawnBeesInTriangle()
    {
        int beesToSpawn = _countBee - _currentBeeCount;
        if (beesToSpawn <= 0) return;

        int rows = Mathf.CeilToInt(Mathf.Sqrt(_countBee));
        int currentIndex = _currentBeeCount;

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j <= i; j++)
            {
                if (currentIndex >= _countBee)
                {
                    break;
                }
                Vector3 spawnPosition = transform.position + _spawnOffset + new Vector3(j, 0, -i);
                if (!_beePositions.Contains(spawnPosition))
                {
                    GameObject bee = Instantiate(_beePrefab, spawnPosition, transform.rotation, transform);
                    _beePositions.Add(spawnPosition);
                    currentIndex++;
                }
            }
        }

        _currentBeeCount = _countBee;
    }

    private void Update()
    {
        _movement.Move(_inputVector);
    }

    private void OnMove(InputValue value)
    {
        _inputVector = value.Get<Vector2>();
    }
}
