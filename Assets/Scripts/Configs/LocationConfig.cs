using UnityEngine;
using Location;

namespace Configs
{
    [CreateAssetMenu(fileName = "Location Level", menuName = "Configs/Location")]
    public class LocationConfig : ScriptableObject
    {
        [Header("Location Parameters")]
        [SerializeField] private int _chunkNumber;
        [Space(height: 5f)]

        [SerializeField] private Chunk _chunkPrefab;
        [SerializeField] private Chunk _finishChunkPrefab;

        #region Public Fields

        public int ChunkNumber => _chunkNumber;
        public Chunk ChunkPrefab => _chunkPrefab;
        public Chunk FinishChunkPrefab => _finishChunkPrefab;

        #endregion
    }
}