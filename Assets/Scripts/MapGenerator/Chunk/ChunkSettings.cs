using UnityEngine;

[CreateAssetMenu(fileName = "ChunkSettings", menuName = "ScriptableObjects/ChunkSettings", order = 1)]
public class ChunkSettings : ScriptableObject
{
    public UnityEngine.GameObject ChunkPrefab => _chunkPrefab;
    public Vector3 MobPosition => _mobPosition;

    [SerializeField] private UnityEngine.GameObject _chunkPrefab;
    [SerializeField] private Vector3 _mobPosition;
}
