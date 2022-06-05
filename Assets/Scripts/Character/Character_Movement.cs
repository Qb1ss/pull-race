using UnityEngine;
using UnityEngine.Events;
using Configs;

namespace Character
{
    [RequireComponent(typeof(Rigidbody), typeof(Transform))]
    public class Character_Movement : MonoBehaviour
    {
        [SerializeField] private CharacterParametersConfig _parameters;
        [SerializeField] private Joystick _joystick;

        #region EVENTS

        public static UnityEvent<int , int> OnRunOutTime = new UnityEvent<int, int>();
        public static UnityEvent OnLoseLevel = new UnityEvent();

        #endregion

        #region CONSTS

        private const float FORCE_ROTATE = 20f;

        #endregion

        private float _movingSpeed;
        private float _constMovingTime;
        private float _slowerMovingTime;
        private float _subtractinSpeedFromTime;

        private int _startZPosition;

        private bool _isActiveGame = false;

        private Transform _transform;
        private Rigidbody _rigidbody;

        #region Private Fields

        private float _movementSpeed => _parameters.MovementSpeed;
        private float _constMovementTime => _parameters.ConstMovementTimer;
        private float _slowerMovementTime => _parameters.SlowerMovementTimer;
        private float _forceTensionSlingshot => _parameters.ForceTensionSlingshot;

        #endregion


        #region MONO

        private void Awake()
        {
            _transform = GetComponent<Transform>();
            _rigidbody = GetComponent<Rigidbody>();
        }


        private void Start()
        {
            _constMovingTime = _constMovementTime;
            _movingSpeed = _movementSpeed;
            _slowerMovingTime = _slowerMovementTime;
            _subtractinSpeedFromTime = _slowerMovingTime / _movingSpeed;

            _startZPosition = (int)_transform.position.z;
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

        private void FixedUpdate()
        {
            if (_isActiveGame == false)
            {
                return;
            }

            Movement();
        }

        #region Private Methods

        private void OnStartGame(float forceTension)
        {
            _isActiveGame = true;

            _rigidbody.isKinematic = false;

            _movingSpeed = _movingSpeed * forceTension * _forceTensionSlingshot;
        }


        private void Movement()
        {
            float direction = _joystick.Horizontal;

            if (_constMovingTime <= 0)
            {
                _movingSpeed -= Time.deltaTime / _subtractinSpeedFromTime;

                if (_slowerMovingTime < 0)
                {
                    return;
                }

                _slowerMovingTime -= Time.deltaTime;            
            }
            else if (_constMovingTime > 0)
            {
                _constMovingTime -= Time.deltaTime;
            }

            if (_movingSpeed < 0)
            {
                _isActiveGame = false;

                OnRunOutTime?.Invoke(_startZPosition, (int)_transform.position.z);
                OnLoseLevel?.Invoke();

                return;
            }

            _transform.localRotation = Quaternion.Euler(0f, direction * FORCE_ROTATE, 0f);
            _transform.Translate(new Vector3(0f, 0f, _movingSpeed * Time.deltaTime));
        }

        #endregion
    }
}