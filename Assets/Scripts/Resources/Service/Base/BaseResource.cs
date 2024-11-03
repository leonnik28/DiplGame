using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using Zenject;

public abstract class BaseResource
{
    public BaseResourceSettings Settings => _settings;

    protected ResourceFactory _resourceFactory;
    protected BaseResourceSettings _settings;

    private readonly int _timeToDestroy = 5;

    private Bag _bag;
    [Inject]
    private void Construct(Bag bag)
    {
        _bag = bag;
    }

    public virtual GameObject Spawn(GameObject gameObject, Vector3 position)
    {
        GameObject spawnObject = _resourceFactory.Create(gameObject, position, Quaternion.identity);
        DestroyAfterDelay(spawnObject).Forget();
        return spawnObject;
    }

    private async UniTaskVoid DestroyAfterDelay(GameObject spawnObject)
    {
        await UniTask.Delay(TimeSpan.FromSeconds(_timeToDestroy));
        Destroy(spawnObject);
    }

    public virtual void Collect()
    {
        _bag.GetResource(this);
    }

    protected virtual void Destroy(GameObject spawnObject)
    {
        if (spawnObject)
        {
            GameObject.Destroy(spawnObject);
        }
    }
}
