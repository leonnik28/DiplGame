using Cysharp.Threading.Tasks;
using UnityEngine;

public class MoneyResource : BaseResource
{
    public MoneySettings MoneySettings => _settings as MoneySettings;

    public MoneyResource(MoneySettings settings, ResourceFactory resourceFactory)
    {
        _settings = settings;
        _resourceFactory = resourceFactory;
    }

    public override async UniTask Spawn(GameObject gameObject, Vector3 position)
    {
        await base.Spawn(gameObject, position);
    }

    protected override void Collect()
    {
    }
}