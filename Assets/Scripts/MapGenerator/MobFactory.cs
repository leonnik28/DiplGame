using UnityEngine;
using Zenject;

public class MobFactory : IFactory<UnityEngine.GameObject, Vector3, UnityEngine.GameObject>
{
    private readonly DiContainer _container;

    public MobFactory(DiContainer container)
    {
        _container = container;
    }

    public UnityEngine.GameObject Create(UnityEngine.GameObject mobPrefab, Vector3 position)
    {
        UnityEngine.GameObject mob = _container.InstantiatePrefab(mobPrefab, position, Quaternion.identity, null);
        return mob;
    }
}
