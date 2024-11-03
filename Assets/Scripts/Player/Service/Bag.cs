using Zenject;

public class Bag
{
    private int _countMoneyInBag;

    private GameSession _gameSession;
    private UserData _userData;

    [Inject]
    private void Construct(GameSession gameSession, UserData userData)
    {
        _gameSession = gameSession;
        _userData = userData;
    }

    public void GetMoney(int money)
    {
        _countMoneyInBag += money;
    }

    public void FoldMoney()
    {
        if (_countMoneyInBag > 0)
        {
            _gameSession.SaveData(countMoney: _userData.Data.countMoney + _countMoneyInBag);
            _countMoneyInBag = 0;
        }
    }
}
