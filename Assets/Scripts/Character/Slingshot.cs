using UnityEngine;

namespace Character.Slingshot
{
    public class Slingshot : MonoBehaviour
    {
        [Header("Parameters")]
        [SerializeField] private Transform _startBorder;
        [SerializeField] private Transform _character;

        [SerializeField] private float _maxDistanceTencion = 4f;

        [SerializeField] private Joystick _joysticForceTencion;

        private Vector3 _startPosition;

        private bool _isStartingGame = false;


        #region MONO

        private void Start()
        {
            _startPosition = _startBorder.position;
        }


        private void OnEnable()
        {
            DynamicJoystick.OnStartGame.AddListener(OnStartGame);
        }


        private void OnDisable()
        {
            DynamicJoystick.OnStartGame.AddListener(OnStartGame);
        }

        #endregion


        private void Update()
        {
            Vector3 newPosition = new Vector3(_startBorder.position.x, _startBorder.position.y, _startBorder.position.z + _joysticForceTencion.Vertical / 20);

            if(newPosition.z < _startPosition.z - _maxDistanceTencion)
            {
                return;
            }

            _startBorder.position = newPosition;

            if(_isStartingGame == true)
            {
                _startBorder.position = _startPosition;

                return;
            }

            newPosition = new Vector3(_character.position.x, _character.position.y, _character.position.y / 2 + newPosition.z);

            _character.position = newPosition;
        }


        private void OnStartGame(float force)
        {
            _isStartingGame = true;
        }
    }
}