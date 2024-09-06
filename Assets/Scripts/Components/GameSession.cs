using System;
using Zenject;

public class GameSession : IInitializable, IDisposable
{
    public Action OnDataLoad;

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

    public void SaveData(UserData.SaveData data)
    {
        _userData.Save(data);
    }

    public void LoadData()
    {
        _userData.Load();
        OnDataLoad?.Invoke();
    }
}
