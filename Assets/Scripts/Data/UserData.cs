using System;
using System.Threading.Tasks;
using Zenject;

public class UserData
{
    [Serializable]
    public struct SaveData
    {
        public int levelBee;
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

    public async Task Save(SaveData data)
    {
        _data = data;
        await _storageService.SaveAsync(_key, data);
    }

    public async Task Load()
    {
        _data = await _storageService.LoadAsync<SaveData>(_key);
    }
}
