using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Chest", menuName = "Chest/Create New Chest")]
public class ChestSettings : ScriptableObject
{
    public List<BaseResourceSettings> Resources { get => _resources; set => _resources = value; }
    public int HealtCount { get => _healtCount; set => _healtCount = value; }

    [SerializeField] private int _healtCount;
    [SerializeField] private List<BaseResourceSettings> _resources;
}
