using UnityEngine;
using Configs;

namespace Location
{
    public class LocationManager : MonoBehaviour
    {
        #region CONSTS

        private const float MULTIPLY_TRACK_LENGTH = 4f;

        #endregion

        [SerializeField] private LocationConfig _parameters;

        [SerializeField] private GameObject _groundChunkPrefab;

        #region Private Fields

        [SerializeField] private Vector3 _heightMainParameters => _parameters.HeightMainParameters;

        [SerializeField] private float heightStartZone => _parameters.HeightStartZone;
        [SerializeField] private float _heightFinishZone => _parameters.HeightFinishZone;

        #endregion


        #region MONO

        private void Start()
        {
            GenerateLocation();
        }

        #endregion

        #region Private Methods

        private void GenerateLocation()
        {
            Vector3 size = new Vector3(_heightMainParameters.x, _heightMainParameters.y, (_heightMainParameters.z + heightStartZone + _heightFinishZone));
            Vector3 position = new Vector3(0f, 0f, (_heightMainParameters.z + heightStartZone + _heightFinishZone) * MULTIPLY_TRACK_LENGTH);

            _groundChunkPrefab.transform.localScale = size;
            _groundChunkPrefab.transform.localPosition = position;
        }

        #endregion
    }
}