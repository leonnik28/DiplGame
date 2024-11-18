using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class ChunksPlacer : MonoBehaviour
{
    [SerializeField] private MapSettings _mapSettings;

    [SerializeField] private Chunk _startingChunk;
    [SerializeField] private GameObject _mobPrefab;
    [SerializeField] private NavMeshSurface _meshSurface;

    private Dictionary<Vector2Int, Chunk> _spawnedChunks;
    private System.Random _random;
    private ChunkFactory _chunkFactory;
    private MobFactory _mobFactory;

    [Inject]
    public void Construct(ChunkFactory chunkFactory, MobFactory mobFactory)
    {
        _chunkFactory = chunkFactory;
        _mobFactory = mobFactory;
    }

    private void Start()
    {
        _meshSurface.BuildNavMesh();
        _random = new System.Random(_mapSettings.Seed);

        _spawnedChunks = new Dictionary<Vector2Int, Chunk>();
        _spawnedChunks[Vector2Int.zero] = _startingChunk;

        int placedChunks = 1;

        while (placedChunks < _mapSettings.RoomCount)
        {
            Vector2Int position = GetRandomPosition();
            if (!_spawnedChunks.ContainsKey(position))
            {
                if (CanPlaceChunk(position))
                {
                    if (PlaceOneChunk(position))
                    {
                        placedChunks++;
                    }
                }
            }
        }
    }

    private Vector2Int GetRandomPosition()
    {
        int x = _random.Next(-_mapSettings.RoomCount, _mapSettings.RoomCount);
        int y = _random.Next(-_mapSettings.RoomCount, _mapSettings.RoomCount);
        return new Vector2Int(x, y);
    }

    private bool CanPlaceChunk(Vector2Int position)
    {
        ChunkSettings chunkSettings = _mapSettings.ChunkSettings[_random.Next(_mapSettings.ChunkSettings.Count)];
        Chunk newChunk = chunkSettings.ChunkPrefab.GetComponent<Chunk>();

        List<Vector2Int> neighbours = new List<Vector2Int>();

        if (newChunk.DoorUp != null && _spawnedChunks.TryGetValue(position + Vector2Int.up, out Chunk upChunk) && upChunk.DoorDown != null) neighbours.Add(Vector2Int.up);
        if (newChunk.DoorDown != null && _spawnedChunks.TryGetValue(position + Vector2Int.down, out Chunk downChunk) && downChunk.DoorUp != null) neighbours.Add(Vector2Int.down);
        if (newChunk.DoorRight != null && _spawnedChunks.TryGetValue(position + Vector2Int.right, out Chunk rightChunk) && rightChunk.DoorLeft != null) neighbours.Add(Vector2Int.right);
        if (newChunk.DoorLeft != null && _spawnedChunks.TryGetValue(position + Vector2Int.left, out Chunk leftChunk) && leftChunk.DoorRight != null) neighbours.Add(Vector2Int.left);

        return neighbours.Count > 0;
    }

    private bool PlaceOneChunk(Vector2Int position)
    {
        ChunkSettings chunkSettings = _mapSettings.ChunkSettings[_random.Next(_mapSettings.ChunkSettings.Count)];
        Vector3 chunkPosition = new Vector3(position.x * chunkSettings.ChunkPrefab.GetComponent<Chunk>().Size.x, 0, position.y * chunkSettings.ChunkPrefab.GetComponent<Chunk>().Size.y);
        GameObject chunkObject = _chunkFactory.Create(chunkSettings, chunkPosition);
        _meshSurface.BuildNavMesh();
        GameObject mobObject = _mobFactory.Create(_mobPrefab, chunkPosition + chunkSettings.MobPosition);
        Chunk newChunk = chunkObject.GetComponent<Chunk>();
        newChunk.RotateRandomly(_random);

        if (ConnectToSomething(newChunk, position))
        {
            _spawnedChunks[position] = newChunk;
            return true;
        }
        return false;
    }

    private bool ConnectToSomething(Chunk chunk, Vector2Int p)
    {
        List<Vector2Int> neighbours = new List<Vector2Int>();

        if (chunk.DoorUp != null && _spawnedChunks.TryGetValue(p + Vector2Int.up, out Chunk upChunk) && upChunk.DoorDown != null) neighbours.Add(Vector2Int.up);
        if (chunk.DoorDown != null && _spawnedChunks.TryGetValue(p + Vector2Int.down, out Chunk downChunk) && downChunk.DoorUp != null) neighbours.Add(Vector2Int.down);
        if (chunk.DoorRight != null && _spawnedChunks.TryGetValue(p + Vector2Int.right, out Chunk rightChunk) && rightChunk.DoorLeft != null) neighbours.Add(Vector2Int.right);
        if (chunk.DoorLeft != null && _spawnedChunks.TryGetValue(p + Vector2Int.left, out Chunk leftChunk) && leftChunk.DoorRight != null) neighbours.Add(Vector2Int.left);

        if (neighbours.Count == 0) return false;

        Vector2Int selectedDirection = neighbours[_random.Next(neighbours.Count)];
        Chunk selectedChunk = _spawnedChunks[p + selectedDirection];

        if (selectedDirection == Vector2Int.up)
        {
            chunk.DoorUp.SetActive(false);
            selectedChunk.DoorDown.SetActive(false);
        }
        else if (selectedDirection == Vector2Int.down)
        {
            chunk.DoorDown.SetActive(false);
            selectedChunk.DoorUp.SetActive(false);
        }
        else if (selectedDirection == Vector2Int.right)
        {
            chunk.DoorRight.SetActive(false);
            selectedChunk.DoorLeft.SetActive(false);
        }
        else if (selectedDirection == Vector2Int.left)
        {
            chunk.DoorLeft.SetActive(false);
            selectedChunk.DoorRight.SetActive(false);
        }

        return true;
    }
}
