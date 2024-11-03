using UnityEngine;
using Zenject;

public class HomeUI : MonoBehaviour
{
    [SerializeField] private int _beeCost;
    [SerializeField] private Player _player;
    [SerializeField] private GameObject _beePrefab;
    [SerializeField] private Vector3 _spawnOffset;

    private UserData _userData;
    private GameSession _gameSession;

    [Inject]
    private void Construct(UserData userData, GameSession gameSession)
    {
        _userData = userData;
        _gameSession = gameSession;
    }

    public void BuyBee()
    {
    }
}
