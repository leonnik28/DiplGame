using System;
using Zenject;

public class GameSession : IInitializable, IDisposable
{
    public Action<UserData.SaveData> OnDataChange;

    private UserData _userData;

    [Inject]
    private void Construct(UserData userData)
    {
        _userData = userData;
    }

    public void Initialize()
    {
        
    }

    public void Dispose()
    {

    }

    public void SaveData(int countBee = 1, int countMoney = -1)
    {
        UserData.SaveData data = new UserData.SaveData() { 
            countBee = countBee,
            countMoney = countMoney
        };
        if (countBee == 1)
        {
            data.countBee = _userData.Data.countBee;
        }
        if (countMoney == -1)
        {
            data.countMoney = _userData.Data.countMoney;
        }
        _userData.Save(data);
        OnDataChange?.Invoke(data);
    }

    public void LoadData()
    {
        _userData.Load();
        OnDataChange?.Invoke(_userData.Data);
    }
}
