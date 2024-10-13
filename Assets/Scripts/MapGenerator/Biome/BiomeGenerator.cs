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
                // Первый биом размещается в начальной точке
                position = new Vector3(hilbertCurve[i].x * sizeX, 0, hilbertCurve[i].y * sizeY);
            }
            else
            {
                // Новый биом размещается рядом с существующими биомами
                position = FindBestPosition(sizeX, sizeY, biomeRects);
            }

            Rect biomeRect = new Rect(position.x, position.z, sizeX, sizeY);

            // Проверка на перекрытие и корректировка позиции
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
                // Попытка найти свободное место рядом с существующими биомами
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

        // Заполнение пустых мест между биомами чанками
        FillEmptySpacesWithChunks(biomeRects);

        MapData mapData = new MapData
        {
            Biomes = biomeDataList
        };

        _mapSaver.SaveMap(mapData);
    }

    private Vector3 FindBestPosition(int sizeX, int sizeY, List<Rect> biomeRects)
    {
        Vector3[] directions = new Vector3[]
        {
            new Vector3(1, 0, 0), // Вправо
            new Vector3(0, 0, 1), // Вверх
            new Vector3(-1, 0, 0), // Влево
            new Vector3(0, 0, -1), // Вниз
        };

        // Перемешиваем направления для случайного выбора
        System.Random random = new System.Random();
        directions = directions.OrderBy(a => random.Next()).ToArray();

        foreach (var rect in biomeRects)
        {
            foreach (var direction in directions)
            {
                Vector3 newPosition = new Vector3(rect.x + direction.x * sizeX, 0, rect.y + direction.z * sizeY);
                Rect newBiomeRect = new Rect(newPosition.x, newPosition.z, sizeX, sizeY);
                bool overlaps = false;

                foreach (var otherRect in biomeRects)
                {
                    if (newBiomeRect.Overlaps(otherRect))
                    {
                        overlaps = true;
                        break;
                    }
                }

                if (!overlaps)
                {
                    return newPosition;
                }
            }
        }

        // Если не найдено подходящее место, возвращаем начальную позицию
        return new Vector3(biomeRects[0].xMax, 0, biomeRects[0].y);
    }

    private void FillEmptySpacesWithChunks(List<Rect> biomeRects)
    {
        // Определяем границы всех биомов
        float minX = biomeRects.Min(r => r.x);
        float maxX = biomeRects.Max(r => r.xMax);
        float minY = biomeRects.Min(r => r.y);
        float maxY = biomeRects.Max(r => r.yMax);

        // Перебираем все клетки в пределах границ
        for (float x = minX; x < maxX; x++)
        {
            for (float y = minY; y < maxY; y++)
            {
                bool isFilled = false;

                foreach (var rect in biomeRects)
                {
                    if (rect.Contains(new Vector2(x, y)))
                    {
                        isFilled = true;
                        break;
                    }
                }

                if (!isFilled)
                {
                    // Создаем чанк в пустой клетке с настройками соседнего биома
                    Vector3 position = new Vector3(x, 0, y);
                    _biomeFactory.Create(_biomeSettings[0], position);
                }
            }
        }
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
        float noiseValue = Mathf.PerlinNoise(index * _perlinScale + _seed, _seed);
        int biomeIndex = Mathf.FloorToInt(noiseValue * _biomeSettings.Length);
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
