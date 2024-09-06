using System;
using Zenject;

public class UserData
{
    [Serializable]
    public struct SaveData
    {
        public int countBee;
        public int countMoney;
    }

    public SaveData Data => _data;

    private SaveData _data;

    private readonly string _key = "SaveData";

    private StorageService _storageService;

    [Inject]
    private void Construct(StorageService storageService)
    {
        _storageService = storageService;
    }

    public async void Save(SaveData data)
    {
        await _storageService.SaveAsync(_key, data);
    }

    public async void Load()
    {
        _data = await _storageService.LoadAsync<SaveData>(_key);
    }
}
