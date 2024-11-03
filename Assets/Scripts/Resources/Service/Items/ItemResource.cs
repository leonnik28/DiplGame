using Cysharp.Threading.Tasks;
using UnityEngine;

public class ItemResource : BaseResource
{
    public ItemSettings ItemSettings => _settings as ItemSettings;

    public ItemResource(ItemSettings settings, ResourceFactory resourceFactory)
    {
        _settings = settings;
        _resourceFactory = resourceFactory;
    }

    public override async UniTask Spawn(GameObject gameObject, Vector3 position)
    {
        await base.Spawn(gameObject, position);
    }
}
