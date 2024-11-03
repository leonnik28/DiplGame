using UnityEngine;

[CreateAssetMenu(fileName = "New Money", menuName = "Money/Create New Money")]
public class MoneySettings : BaseResourceSettings
{
    public override System.Type ResourceType => typeof(MoneyResource);

    public Type MoneyType => _type;
    public int Count;

    public override GameObject SpawnObject
    {
        get => _spawnObject;
        protected set => _spawnObject = value;
    }

    [SerializeField] private Type _type;
    
    public enum Type
    {
        Bronze,
        Silver,
        Gold,
        Platinum
    }
}
