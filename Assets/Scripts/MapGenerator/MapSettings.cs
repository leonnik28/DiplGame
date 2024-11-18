using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapData", menuName = "Map/Create New Map Settings", order = 1)]
public class MapSettings : ScriptableObject
{
    public int RoomCount { get => _roomCount; set => _roomCount = value; }
    public List<ChunkSettings> ChunkSettings { get => _chunkSettings; set => _chunkSettings = value; }
    public int Seed { get => _seed; set => _seed = value; }

    [SerializeField] private int _roomCount;
    [SerializeField] private List<ChunkSettings> _chunkSettings;
    [SerializeField] private int _seed;
}
