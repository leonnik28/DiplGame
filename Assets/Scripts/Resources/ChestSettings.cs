using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Chest", menuName = "Chest/Create New Chest")]
public class ChestSettings : ScriptableObject
{
    public int HealtCount;
    public List<BaseResourceSettings> Resources;
}
