using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class DataView : MonoBehaviour
{
    [SerializeField] private Text _moneyText;

    private GameSession _gameSession;

    [Inject]
    private void Construct(GameSession gameSession)
    {
        _gameSession = gameSession;
    }

    private void OnEnable()
    {
        _gameSession.OnDataChange += UpdateView;
    }

    private void OnDisable()
    {
        _gameSession.OnDataChange -= UpdateView;
    }

    private void UpdateView(UserData.SaveData data)
    {
    }
}
