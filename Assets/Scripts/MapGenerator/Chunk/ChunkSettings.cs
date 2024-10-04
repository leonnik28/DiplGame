using UnityEngine;

[CreateAssetMenu(fileName = "New Chunk", menuName = "Chunk/Create New Chunk Settings")]
public class ChunkSettings : ScriptableObject
{
    public int SizeX => _sizeX;
    public int SizeY => _sizeY;

    [SerializeField] private int _sizeX;
    [SerializeField] private int _sizeY;

    [SerializeField] private GameObject _landscapePrefab;

    public GameObject LandscapePrefab => _landscapePrefab;
}
