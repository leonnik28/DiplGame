using UnityEngine;
using Zenject;

public class BiomeFactory : IFactory<BiomeSettings, Vector3, int, int, GameObject>
{
    private readonly DiContainer _container;
    private readonly ChunkFactory _chunkFactory;

    public BiomeFactory(DiContainer container, ChunkFactory chunkFactory)
    {
        _container = container;
        _chunkFactory = chunkFactory;
    }

    public GameObject Create(BiomeSettings biomeSettings, Vector3 position, int sizeX, int sizeY)
    {
        GameObject biomeObject = new GameObject(biomeSettings.BiomeName);
        biomeObject.transform.position = position;

        int chunkSizeX = biomeSettings.ChunkSettings.SizeX;
        int chunkSizeY = biomeSettings.ChunkSettings.SizeY;

        int order = Mathf.CeilToInt(Mathf.Log(Mathf.Max(sizeX, sizeY), 2));
        Vector2Int[] hilbertCurve = HilbertCurve.GenerateHilbertCurve(order);

        foreach (var point in hilbertCurve)
        {
            if (point.x < sizeX && point.y < sizeY)
            {
                Vector3 chunkPosition = new Vector3(position.x + point.x * chunkSizeX, 0, position.z + point.y * chunkSizeY);
                GameObject chunkObject = _chunkFactory.Create(biomeSettings.ChunkSettings, chunkPosition, biomeSettings);
                chunkObject.transform.SetParent(biomeObject.transform);
            }
        }

        return biomeObject;
    }

    public GameObject Create(BiomeSettings biomeSettings, Vector3 position)
    {
        GameObject biomeObject = new GameObject(biomeSettings.BiomeName);
        biomeObject.transform.position = position;

        int chunkSizeX = biomeSettings.ChunkSettings.SizeX;
        int chunkSizeY = biomeSettings.ChunkSettings.SizeY;

        int order = Mathf.CeilToInt(Mathf.Log(Mathf.Max(chunkSizeX, chunkSizeY), 2));
        Vector2Int[] hilbertCurve = HilbertCurve.GenerateHilbertCurve(order);

        foreach (var point in hilbertCurve)
        {
            if (point.x < chunkSizeX && point.y < chunkSizeY)
            {
                Vector3 chunkPosition = new Vector3(position.x + point.x * chunkSizeX, 0, position.z + point.y * chunkSizeY);
                GameObject chunkObject = _chunkFactory.Create(biomeSettings.ChunkSettings, chunkPosition, biomeSettings);
                chunkObject.transform.SetParent(biomeObject.transform);
            }
        }

        return biomeObject;
    }
}
