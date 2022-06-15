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

        [SerializeField] private Obstruction[] _obstructionsPrefabs;

        #region Private Fields
        public float XSpawnerPosition => _xSpawnerPosition;

        public Obstruction[] ObstructionsPrefabs => _obstructionsPrefabs;

        #endregion
    }
}