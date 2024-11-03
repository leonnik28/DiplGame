using UnityEngine;
using Zenject;

public class ChunkFactory : IFactory<ChunkSettings, Vector3, GameObject>
{
    private readonly DiContainer _container;

    public ChunkFactory(DiContainer container)
    {
        _container = container;
    }

    public GameObject Create(ChunkSettings chunkSettings, Vector3 position)
    {
        GameObject chunk = _container.InstantiatePrefab(chunkSettings.ChunkPrefab, position, Quaternion.identity, null);
        return chunk;
    }
}
