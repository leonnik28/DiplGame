using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Chest", menuName = "Chest/Create New Chest")]
public class ChestSettings : ScriptableObject
{
    public List<BaseResourceSettings> Resources { get => _resources; set => _resources = value; }
    public int Healt { get => _healt; set => _healt = value; }

    [SerializeField] private int _healt;
    [SerializeField] private List<BaseResourceSettings> _resources;
}
