using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;
public class BiomeGenerator : MonoBehaviour
{
    [SerializeField] private BiomeSettings[] _biomeSettings;
    [SerializeField] private int _biomeCount;
    [SerializeField] private float _perlinScale = 0.1f;
    [SerializeField] private int _seed;

    [Inject] private BiomeFactory _biomeFactory;
    [Inject] private MapSaver _mapSaver;

    private void Start()
    {
        MapData mapData = _mapSaver.LoadMap();
        GenerateBiomes();
    }

    public void GenerateBiomes()
    {
        List<MapData.BiomeData> biomeDataList = new List<MapData.BiomeData>();
        List<Rect> biomeRects = new List<Rect>();
        int order = Mathf.CeilToInt(Mathf.Log(_biomeCount, 2));
        Vector2Int[] hilbertCurve = HilbertCurve.GenerateHilbertCurve(order);
        for (int i = 0; i < _biomeCount; i++)
        {
            BiomeSettings biomeSettings = GetBiomeSettingsByPerlinNoise(i);
            int sizeX = Random.Range(biomeSettings.MinSizeX, biomeSettings.MaxSizeX);
            int sizeY = Random.Range(biomeSettings.MinSizeY, biomeSettings.MaxSizeY);
            Vector3 position;
            if (biomeRects.Count == 0)
            {
                position = new Vector3(hilbertCurve[i].x * sizeX, 0, hilbertCurve[i].y * sizeY);
            }
            else
            {
                Rect lastBiomeRect = biomeRects[biomeRects.Count - 1];
                position = FindBestPosition(lastBiomeRect, sizeX, sizeY, biomeRects);
            }
            Rect biomeRect = new Rect(position.x, position.z, sizeX, sizeY);
            
            bool overlaps = false;
            foreach (var rect in biomeRects)
            {
                if (biomeRect.Overlaps(rect))
                {
                    overlaps = true;
                    break;
                }
            }
            if (!overlaps)
            {
                GameObject biomeObject = _biomeFactory.Create(biomeSettings, position, sizeX, sizeY);
                biomeDataList.Add(new MapData.BiomeData
                {
                    BiomeName = biomeSettings.BiomeName,
                    Position = position
                });
                biomeRects.Add(biomeRect);
            }
            else
            {
                bool placed = false;
                foreach (var rect in biomeRects)
                {
                    Vector3 newPosition = new Vector3(rect.xMax, 0, rect.y);
                    Rect newBiomeRect = new Rect(newPosition.x, newPosition.z, sizeX, sizeY);
                    bool newOverlaps = false;
                    foreach (var otherRect in biomeRects)
                    {
                        if (newBiomeRect.Overlaps(otherRect))
                        {
                            newOverlaps = true;
                            break;
                        }
                    }
                    if (!newOverlaps)
                    {
                        position = newPosition;
                        biomeRect = newBiomeRect;
                        placed = true;
                        break;
                    }
                }
                if (!placed)
                {
                    foreach (var rect in biomeRects)
                    {
                        Vector3 newPosition = new Vector3(rect.x, 0, rect.yMax);
                        Rect newBiomeRect = new Rect(newPosition.x, newPosition.z, sizeX, sizeY);
                        bool newOverlaps = false;
                        foreach (var otherRect in biomeRects)
                        {
                            if (newBiomeRect.Overlaps(otherRect))
                            {
                                newOverlaps = true;
                                break;
                            }
                        }
                        if (!newOverlaps)
                        {
                            position = newPosition;
                            biomeRect = newBiomeRect;
                            placed = true;
                            break;
                        }
                    }
                }
                if (placed)
                {
                    GameObject biomeObject = _biomeFactory.Create(biomeSettings, position, sizeX, sizeY);
                    biomeDataList.Add(new MapData.BiomeData
                    {
                        BiomeName = biomeSettings.BiomeName,
                        Position = position
                    });
                    biomeRects.Add(biomeRect);
                }
            }
        }
        MapData mapData = new MapData
        {
            Biomes = biomeDataList
        };
        _mapSaver.SaveMap(mapData);
    }

    private Vector3 FindBestPosition(Rect lastBiomeRect, int sizeX, int sizeY, List<Rect> biomeRects)
    {
        Vector3[] directions = new Vector3[]
        {
        new Vector3(lastBiomeRect.xMax, 0, lastBiomeRect.y), 
        new Vector3(lastBiomeRect.x, 0, lastBiomeRect.yMax),
        new Vector3(lastBiomeRect.xMin - sizeX, 0, lastBiomeRect.y), 
        new Vector3(lastBiomeRect.x, 0, lastBiomeRect.yMin - sizeY), 
        };

        System.Random random = new System.Random();
        directions = directions.OrderBy(a => random.Next()).ToArray();
        foreach (var direction in directions)
        {
            Rect newBiomeRect = new Rect(direction.x, direction.z, sizeX, sizeY);
            bool overlaps = false;
            foreach (var rect in biomeRects)
            {
                if (newBiomeRect.Overlaps(rect))
                {
                    overlaps = true;
                    break;
                }
            }
            if (!overlaps)
            {
                return direction;
            }
        }
        return new Vector3(lastBiomeRect.xMax, 0, lastBiomeRect.y);
    }

    private void LoadBiomes(MapData mapData)
    {
        foreach (var biomeData in mapData.Biomes)
        {
            BiomeSettings biome = GetBiomeByName(biomeData.BiomeName);
            if (biome != null)
            {
                _biomeFactory.Create(biome, biomeData.Position);
            }
        }
    }

    private BiomeSettings GetBiomeSettingsByPerlinNoise(int index)
    {
        // Генерируем значение шума Перлина
        float noiseValue = Mathf.PerlinNoise(index * _perlinScale + _seed, _seed);

        // Нормализуем значение шума Перлина (оно уже находится в диапазоне [0, 1])
        float normalizedNoiseValue = noiseValue;

        // Масштабируем нормализованное значение шума до диапазона индексов биомов
        int biomeIndex = Mathf.FloorToInt(normalizedNoiseValue * (_biomeSettings.Length));

        // Возвращаем соответствующий биом сеттинг
        return _biomeSettings[biomeIndex];
    }


    private BiomeSettings GetBiomeByName(string biomeName)
    {
        foreach (var biome in _biomeSettings)
        {
            if (biome.BiomeName == biomeName)
            {
                return biome;
            }
        }
        return null;
    }
}