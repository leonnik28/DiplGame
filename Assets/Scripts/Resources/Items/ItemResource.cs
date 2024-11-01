using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

public class ItemResource : BaseResource
{
    public ItemSettings ItemSettings => _settings;

    private ItemSettings _settings;

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
