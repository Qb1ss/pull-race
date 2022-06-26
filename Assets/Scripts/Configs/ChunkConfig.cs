using UnityEngine;
using Obstructions;

namespace Configs
{
    [CreateAssetMenu(fileName = "Chunk", menuName = "Configs/Chunk")]
    public class ChunkConfig : ScriptableObject
    {
        [Header("Chunk Parameters")]
        [SerializeField] private float _xSpawnerPosition = 6.5f;
        [Space(height: 5f)]

        [Header("Obstructions")]
        [SerializeField] private Obstruction[] _blockPrefabs;
        [SerializeField] private Obstruction[] _carPrefabs;

        #region Private Fields
        public float XSpawnerPosition => _xSpawnerPosition;

        public Obstruction[] ObstructionsBlockPrefabs => _blockPrefabs;
        public Obstruction[] ObstructionsCarPrefabs => _carPrefabs;

        #endregion
    }
}