using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ResourceFactory : IFactory<GameObject, Vector3, Quaternion, GameObject>
{
    private readonly DiContainer _container;

    public ResourceFactory(DiContainer container)
    {
        _container = container;
    }

    public GameObject Create(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        GameObject resource = _container.InstantiatePrefab(prefab, position, rotation, null);
        return resource;
    }
}
