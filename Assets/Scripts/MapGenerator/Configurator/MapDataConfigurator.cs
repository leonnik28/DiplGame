using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MapDataConfigurator : IInitializable
{
    private MapSettings _mapSettings;

    private int _randomRoomCount;
    private int _randomSeed;

    public MapDataConfigurator(MapSettings mapSettings)
    {
        _mapSettings = mapSettings;
    }

    public void Initialize()
    {
        _randomRoomCount = Random.Range(10, 50);
        _randomSeed = Random.Range(1, 5000);
    }

    public void ConfigureMapData()
    {
        _mapSettings.RoomCount = _randomRoomCount;
        _mapSettings.Seed = _randomSeed;
    }

    public void ConfigureMapData(int roomCount, List<ChunkSettings> chunkSettings, int seed)
    {
        _mapSettings.RoomCount = roomCount;
        _mapSettings.Seed = seed;
        _mapSettings.ChunkSettings = chunkSettings;
    }
}
