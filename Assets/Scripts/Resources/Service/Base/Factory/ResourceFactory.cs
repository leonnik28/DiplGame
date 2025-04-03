using UnityEngine;
using Zenject;

public class ResourceFactory : IFactory<UnityEngine.GameObject, Vector3, Quaternion, UnityEngine.GameObject>
{
    private readonly DiContainer _container;

    public ResourceFactory(DiContainer container)
    {
        _container = container;
    }

    public UnityEngine.GameObject Create(UnityEngine.GameObject prefab, Vector3 position, Quaternion rotation)
    {
        UnityEngine.GameObject resource = _container.InstantiatePrefab(prefab, position, rotation, null);
        return resource;
    }
}
