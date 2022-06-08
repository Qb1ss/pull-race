using UnityEngine;
using Obstructions;
using Configs;

namespace Location
{
    public enum TypePosition
    {
        Left,
        Center,
        Right,
        Non
    }

    public class Chunk : MonoBehaviour
    {
        [SerializeField] private ChunkConfig _parameters;

        [Header("Parameters")]
        public Transform Start;
        public Transform End;
        [Space(height: 5f)]

        [SerializeField] private ObstructionTypes _obstructionTypes;
        [SerializeField] private TypePosition _typePosition;
        [Space(height: 5f)]
 

        private float _xSpawnerPosition = 7f;
        private float _xPosition = 0f;

        #region Private Methods

        private Obstruction[] _obstructions => _parameters.ObstructionsPrefabs;

        #endregion


        #region Public Methods

        public void Create()
        {
            Obstruction obstruction = null;

            if (_obstructionTypes == ObstructionTypes.Block)
            {
                obstruction = Instantiate(_obstructions[(int)_obstructionTypes]);
            }
            else if (_obstructionTypes == ObstructionTypes.Car)
            {
                obstruction = Instantiate(_obstructions[Random.Range(1, _obstructions.Length)]);
            }

            if (_typePosition == TypePosition.Left)
            {
                _xPosition = -_xSpawnerPosition;
            }
            else if (_typePosition == TypePosition.Center)
            {
                _xPosition = 0f;
            }
            else if (_typePosition == TypePosition.Right)
            {
                _xPosition = _xSpawnerPosition;
            }

            obstruction.gameObject.transform.position = new Vector3(_xPosition, gameObject.transform.position.y + obstruction.gameObject.transform.position.y / 2, gameObject.transform.position.z);

            if(_typePosition == TypePosition.Non)
            {
                Destroy(obstruction.gameObject);
            }
        }

        #endregion
    }
}