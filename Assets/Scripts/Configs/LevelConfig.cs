using UnityEngine;
using Location;

namespace Configs
{
    [CreateAssetMenu(fileName = "Level", menuName = "Configs/Level")]
    public class LevelConfig : ScriptableObject
    {
        [Header("Location Parameters")]
        [SerializeField] private Chunk[] _chunkPrefab;
        [SerializeField] private Chunk _finishChunkPrefab;

        #region Public Fields

        public Chunk[] ChunkPrefab => _chunkPrefab;
        public Chunk FinishChunkPrefab => _finishChunkPrefab;

        #endregion
    }
}