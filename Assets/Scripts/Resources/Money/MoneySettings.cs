using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Money", menuName = "Money/Create New Money")]
public class MoneySettings : BaseResourceSettings
{
    public Type MoneyType => _type;
    public int Count;

    [SerializeField] private Type _type;
    
    public enum Type
    {
        Bronze,
        Silver,
        Gold,
        Platinum
    }
}
