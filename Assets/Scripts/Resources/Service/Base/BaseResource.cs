using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

public abstract class BaseResource
{
    public BaseResourceSettings Settings => _settings;

    protected ResourceFactory _resourceFactory;
    protected BaseResourceSettings _settings;

    private readonly int _timeToDestroy = 5;

    public virtual async UniTask Spawn(GameObject gameObject, Vector3 position)
    {
        GameObject spawnObject = _resourceFactory.Create(gameObject, position, Quaternion.identity);
        await UniTask.Delay(TimeSpan.FromSeconds(_timeToDestroy));
        Destroy(spawnObject);
    }

    protected virtual void Collect()
    {

    }

    protected virtual void Destroy(GameObject spawnObject)
    {
        GameObject.Destroy(spawnObject);
        spawnObject = null;
    }
}
