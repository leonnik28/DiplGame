using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class ResourcePresenter : IInitializable
{
    private readonly List<BaseResourceSettings> _settings;
    private readonly List<BaseResource> resources = new();
    private readonly DiContainer _container;

    public ResourcePresenter(List<BaseResourceSettings> settings, DiContainer diContainer)
    {
        _settings = settings;
        _container = diContainer;
    }

    public void Initialize()
    {
        foreach (var item in _settings)
        {
            var resource = (BaseResource)_container.Instantiate(item.ResourceType, new object[] { item });
            resources.Add(resource);
        }
    }

    public void SpawnItem(Vector3 position, BaseResourceSettings baseResourceSettings = null)
    {
        BaseResource targetResource = resources.FirstOrDefault(r => r.Settings == baseResourceSettings);

        if (targetResource != null)
        {
            GameObject spawnObject = targetResource.Spawn(targetResource.Settings.SpawnObject, position);
            var resourceObject = spawnObject.AddComponent<ResourceObject>();
            resourceObject.Instantiate(targetResource);
            _container.Inject(resourceObject);
        }
    }
}
