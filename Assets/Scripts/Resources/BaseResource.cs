using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using Zenject;

public abstract class BaseResource
{
    private readonly int _timeToDestroy = 5;

    protected GameObject _gameObject;

    protected ResourceFactory _resourceFactory;

    public virtual async UniTask Spawn(GameObject gameObject, Vector3 position)
    {
        _gameObject = _resourceFactory.Create(gameObject, position, Quaternion.identity);
        await UniTask.Delay(TimeSpan.FromSeconds(_timeToDestroy));
        Destroy();
    }

    protected virtual void Collect()
    {

    }

    protected virtual void Destroy()
    {
        GameObject.Destroy(_gameObject);
    }
}
