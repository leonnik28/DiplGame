using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zenject;

public class UserData
{
    [Serializable]
    public struct SaveData
    {
        public List<BaseResource> resources;
    }

    public SaveData Data 
    { 
        get => _data; 
        set => _data = value; 
    }

    private SaveData _data;

    private readonly string _key = "SaveData";

    private StorageService _storageService;

    [Inject]
    private void Construct(StorageService storageService)
    {
        _storageService = storageService;
    }

    public async UniTask Save(SaveData data)
    {
        _data = data;
        await _storageService.SaveAsync(_key, data);
    }

    public async UniTask Load()
    {
        _data = await _storageService.LoadAsync<SaveData>(_key);
    }
}
