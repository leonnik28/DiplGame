using UnityEngine;

[CreateAssetMenu(fileName = "New Biome", menuName = "Biome/Create New Biome Settings")]
public class BiomeSettings : ScriptableObject
{
    public string BiomeName;
    public ChunkSettings ChunkSettings;
    public GameObject[] EnvironmentObjects;
    public float ObjectDensity;
    public int MinSizeX;
    public int MaxSizeX;
    public int MinSizeY;
    public int MaxSizeY;
}
