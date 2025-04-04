using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using Zenject;

public abstract class BaseResource
{
    public BaseResourceSettings Settings => _settings;

    protected ResourceFactory _resourceFactory;
    protected BaseResourceSettings _settings;

    private readonly int _timeToDestroy = 15;

    private Bag _bag;
    [Inject]
    private void Construct(Bag bag)
    {
        _bag = bag;
    }

    public virtual UnityEngine.GameObject Spawn(UnityEngine.GameObject gameObject, Vector3 position)
    {
        UnityEngine.GameObject spawnObject = _resourceFactory.Create(gameObject, position, Quaternion.identity);
        DestroyAfterDelay(spawnObject).Forget();
        return spawnObject;
    }

    public virtual void Collect()
    {
        _bag.GetResource(this);
    }

    protected virtual void Destroy(UnityEngine.GameObject spawnObject)
    {
        if (spawnObject)
        {
            UnityEngine.GameObject.Destroy(spawnObject);
        }
    }

    private async UniTaskVoid DestroyAfterDelay(UnityEngine.GameObject spawnObject)
    {
        await UniTask.Delay(TimeSpan.FromSeconds(_timeToDestroy));
        Destroy(spawnObject);
    }
}
