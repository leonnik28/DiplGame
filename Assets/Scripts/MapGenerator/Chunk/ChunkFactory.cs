using UnityEngine;
using Zenject;

public class ChunkFactory : IFactory<ChunkSettings, Vector3, BiomeSettings, GameObject>
{
    private readonly DiContainer _container;

    public ChunkFactory(DiContainer container)
    {
        _container = container;
    }

    public GameObject Create(ChunkSettings chunkSettings, Vector3 position, BiomeSettings biome)
    {
        GameObject landscape = _container.InstantiatePrefab(chunkSettings.LandscapePrefab, position, Quaternion.identity, null);
        GenerateEnvironmentObjects(landscape, biome);
        return landscape;
    }

    private void GenerateEnvironmentObjects(GameObject landscape, BiomeSettings biome)
    {
        if (biome.EnvironmentObjects.Length > 0)
        {
            float density = biome.ObjectDensity;
            int sizeX = (int)landscape.transform.localScale.x;
            int sizeY = (int)landscape.transform.localScale.z;

            for (int x = 0; x < sizeX; x++)
            {
                for (int y = 0; y < sizeY; y++)
                {
                    if (Random.value < density)
                    {
                        GameObject environmentObject = biome.EnvironmentObjects[Random.Range(0, biome.EnvironmentObjects.Length)];
                        _container.InstantiatePrefab(environmentObject, new Vector3(x, 0, y) + landscape.transform.position, Quaternion.identity, landscape.transform);
                    }
                }
            }
        }
    }
}
