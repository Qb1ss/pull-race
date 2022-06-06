using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "Location Level", menuName = "Configs/Location")]
    public class LocationConfig : ScriptableObject
    {
        [Header("Location Parameters")]
        [SerializeField] private int _chunkNumber;

        #region Public Fields

        public int ChunkNumber => _chunkNumber;

        #endregion
    }
}