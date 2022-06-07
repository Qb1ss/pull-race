using UnityEngine;
using Obstructions;

namespace Configs
{
    [CreateAssetMenu(fileName = "Chunk", menuName = "Configs/Chunk")]
    public class ChunkConfig : ScriptableObject
    {
        [Header("Chunk Parameters")]
        [SerializeField] private Obstruction[] _obstructionsPrefabs;

        #region Private Fields

        public Obstruction[] ObstructionsPrefabs => _obstructionsPrefabs;

        #endregion
    }
}