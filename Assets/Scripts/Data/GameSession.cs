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
        LoadData();
    }

    public void Dispose()
    {
        SaveData();
    }

    public async void SaveData(int level = 1, int countMoney = -1)
    {
        UserData.SaveData data = new UserData.SaveData() { 
            levelBee = level,
            countMoney = countMoney
        };
        if (level == 1)
        {
            data.levelBee = _userData.Data.levelBee;
        }
        if (countMoney == -1)
        {
            data.countMoney = _userData.Data.countMoney;
        }
        await _userData.Save(data);
        OnDataChange?.Invoke(data);
    }

    public async void LoadData()
    {
        await _userData.Load();
        OnDataChange?.Invoke(_userData.Data);
    }
}
