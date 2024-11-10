using UnityEngine;
using Zenject;

public class MobFactory : IFactory<GameObject, Vector3, GameObject>
{
    private readonly DiContainer _container;

    public MobFactory(DiContainer container)
    {
        _container = container;
    }

    public GameObject Create(GameObject mobPrefab, Vector3 position)
    {
        GameObject mob = _container.InstantiatePrefab(mobPrefab, position, Quaternion.identity, null);
        return mob;
    }
}
