using UnityEngine;

[CreateAssetMenu(fileName = "ChunkSettings", menuName = "ScriptableObjects/ChunkSettings", order = 1)]
public class ChunkSettings : ScriptableObject
{
    public GameObject ChunkPrefab => _chunkPrefab;
    public Vector3 MobPosition => _mobPosition;

    [SerializeField] private GameObject _chunkPrefab;
    [SerializeField] private Vector3 _mobPosition;
}
