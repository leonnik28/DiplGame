using Zenject;

public class Bag
{
    private int _countMoneyInBag;

    private GameSession _gameSession;

    [Inject]
    private void Construct(GameSession gameSession)
    {
        _gameSession = gameSession;
    }

    public void FoldMoney()
    {
        if (_countMoneyInBag > 0)
        {
            _gameSession.SaveData(countMoney: _countMoneyInBag);
            _countMoneyInBag = 0;
        }
    }
}
