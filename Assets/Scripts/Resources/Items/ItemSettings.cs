using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item/Create New Item")]
public class ItemSettings : BaseResourceSettings
{
    public Type ItemType => _type;
    public MoneySettings Price;
    public int Level;
    public GameObject GameObject;

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
