using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "Location Level", menuName = "Configs/Location")]
    public class LocationConfig : ScriptableObject
    {
        [Header("Location Parameters")]
        [SerializeField] private Vector3 _heightMainParameters;
        [Space(height: 5f)]

        [SerializeField] private float _heightStartZone = 10;
        [SerializeField] private float _heightFinishZone = 10;

        #region Public Fields

        public Vector3 HeightMainParameters => _heightMainParameters;
        public float HeightStartZone => _heightStartZone;
        public float HeightFinishZone => _heightFinishZone;

        #endregion
    }
}