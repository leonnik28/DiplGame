using UnityEngine;

[CreateAssetMenu(fileName = "New Money", menuName = "Money/Create New Money")]
public class MoneySettings : BaseResourceSettings
{
    public override System.Type ResourceType => typeof(MoneyResource);

    public Type MoneyType { get => _type; set => _type = value; }
    public int Count { get => _count; set => _count = value; }
    public override GameObject SpawnObject { get => _spawnObject; protected set => _spawnObject = value; }

    [SerializeField] private Type _type;

    [Range(1, 100)]
    [SerializeField] private int _count;
    
    public enum Type
    {
        Bronze,
        Silver,
        Gold,
        Platinum
    }
}
