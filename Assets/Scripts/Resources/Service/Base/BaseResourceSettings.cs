using UnityEngine;

public abstract class BaseResourceSettings : ScriptableObject
{
    public abstract System.Type ResourceType { get; }

    public virtual UnityEngine.GameObject SpawnObject { get; protected set; }
    [SerializeField] protected UnityEngine.GameObject _spawnObject;
}
