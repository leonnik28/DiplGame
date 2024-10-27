using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class ChunksPlacer : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] private int _roomCount;
    [SerializeField] private List<ChunkSettings> _chunkSettings;
    [SerializeField] private Chunk _startingChunk;
    [SerializeField] private int _seed;

    private Dictionary<Vector2Int, Chunk> _spawnedChunks;
    private System.Random _random;
    private ChunkFactory _chunkFactory;

    [Inject]
    public void Construct(ChunkFactory chunkFactory)
    {
        _chunkFactory = chunkFactory;
    }

    private void Start()
    {
        _random = new System.Random(_seed);

        _spawnedChunks = new Dictionary<Vector2Int, Chunk>();
        _spawnedChunks[Vector2Int.zero] = _startingChunk;

        int placedChunks = 1;

        while (placedChunks < _roomCount)
        {
            Vector2Int position = GetRandomPosition();
            if (!_spawnedChunks.ContainsKey(position))
            {
                if (PlaceOneChunk(position))
                {
                    placedChunks++;
                }
            }
        }

        Debug.Log($"Placed {placedChunks} chunks out of {_roomCount} requested.");
    }

    private Vector2Int GetRandomPosition()
    {
        int x = _random.Next(-_roomCount, _roomCount);
        int y = _random.Next(-_roomCount, _roomCount);
        return new Vector2Int(x, y);
    }

    private bool PlaceOneChunk(Vector2Int position)
    {
        ChunkSettings chunkSettings = _chunkSettings[_random.Next(_chunkSettings.Count)];
        Vector3 chunkPosition = new Vector3(position.x * chunkSettings.ChunkPrefab.GetComponent<Chunk>().Size.x, 0, position.y * chunkSettings.ChunkPrefab.GetComponent<Chunk>().Size.y);
        GameObject chunkObject = _chunkFactory.Create(chunkSettings, chunkPosition);
        Chunk newChunk = chunkObject.GetComponent<Chunk>();
        newChunk.RotateRandomly(_random);

        if (ConnectToSomething(newChunk, position))
        {
            _spawnedChunks[position] = newChunk;
            return true;
        }
        else
        {
            Destroy(newChunk.gameObject);
            return false;
        }
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
