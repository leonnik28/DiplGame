using UnityEngine;

public abstract class BaseResourceSettings : ScriptableObject
{
    public abstract System.Type ResourceType { get; }

    public virtual GameObject SpawnObject { get; protected set; }
    [SerializeField] protected GameObject _spawnObject;
}
