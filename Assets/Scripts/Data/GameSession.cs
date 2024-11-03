using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
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
        _ = LoadData();
    }

    public void Dispose()
    {
        
    }

    public async UniTaskVoid SaveData(List<BaseResource> baseResources)
    {
        UserData.SaveData data = new UserData.SaveData()
        {
            resources = _userData.Data.resources
        };
        data.resources.AddRange(baseResources);
        _userData.Data = data;
        await _userData.Save(data);
        OnDataChange?.Invoke(data);
    }


    public async UniTaskVoid LoadData()
    {
        await _userData.Load();
        OnDataChange?.Invoke(_userData.Data);
    }
}
