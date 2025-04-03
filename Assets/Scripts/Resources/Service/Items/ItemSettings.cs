using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item/Create New Item")]
public class ItemSettings : BaseResourceSettings
{
    public override System.Type ResourceType => typeof(ItemResource);

    public Type ItemType => _type;
    public MoneySettings Price;
    public int Level;

    public override UnityEngine.GameObject SpawnObject { get => _spawnObject; protected set => _spawnObject = value; }

    [SerializeField] private Type _type;

    public enum Type
    {
        Sword,
        Helmet,
        Cuirass,
        Pants,
        Boots,
        Belt,
        Gloves,
        Ring,
        Medallion
    }
}
