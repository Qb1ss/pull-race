using UnityEngine;
using Obstructions;

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
        [Header("Parameters")]
        public Transform Start;
        public Transform End;
        [Space(height: 5f)]

        [SerializeField] private TypePosition _typePosition;
        [Space(height: 5f)]

        [SerializeField] private Obstruction _obstructions = null;

        private float _xSpawnerPosition = 8f;
        private float _xPosition = 0f;


        #region Public Methods

        public void Create()
        {
            Obstruction obstruction = Instantiate(_obstructions);

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
                obstruction.gameObject.SetActive(false);
            }
        }

        #endregion
    }
}