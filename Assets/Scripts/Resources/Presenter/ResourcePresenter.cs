using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class ResourcePresenter : MonoBehaviour
{
    [SerializeField] private List<BaseResourceSettings> _settings;

    private readonly List<BaseResource> resources = new List<BaseResource>();

    [Inject]
    private readonly DiContainer _container;

    private void Awake()
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