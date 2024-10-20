using UnityEngine;

[CreateAssetMenu(fileName = "ChunkSettings", menuName = "ScriptableObjects/ChunkSettings", order = 1)]
public class ChunkSettings : ScriptableObject
{
    public GameObject ChunkPrefab => _chunkPrefab;

    [SerializeField] private GameObject _chunkPrefab;
}
