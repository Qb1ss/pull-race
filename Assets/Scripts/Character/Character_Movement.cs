using UnityEngine;
using UnityEngine.Events;
using Configs;

namespace Character
{
    [RequireComponent(typeof(Rigidbody), typeof(Transform))]
    public class Character_Movement : MonoBehaviour
    {
        [SerializeField] private CharacterParametersConfig _parameters;

        #region EVENTS

        public static UnityEvent OnRunOutTime = new UnityEvent();

        #endregion

        #region CONSTS

        private const float FORCE_ROTATE = 20f;

        #endregion

        private float _movingSpeed;
        private float _constMovingTime;
        private float _slowerMovingTime;
        private float _subtractinSpeedFromTime;

        private bool _isActiveGame = false;

        private Transform _transform;
        private Joystick _joystick;

        #region Private Fields

        private float _movementSpeed => _parameters.MovementSpeed;

        private float _constMovementTime => _parameters.ConstMovementTimer;
        private float _slowerMovementTime => _parameters.SlowerMovementTimer;

        #endregion


        #region MONO

        private void Awake()
        {
            _transform = GetComponent<Transform>();

            _joystick = FindObjectOfType<Joystick>();
        }


        private void Start()
        {
            _movingSpeed = _movementSpeed;
            _constMovingTime = _constMovementTime;
            _slowerMovingTime = _slowerMovementTime;
            _subtractinSpeedFromTime = _slowerMovingTime / _movingSpeed;
        }


        private void OnEnable()
        {
            //пуск рогатки
            Test.OnStartGame.AddListener(() => _isActiveGame = true);
        }


        private void OnDisable()
        {
            //пуск рогатки
        }

        #endregion

        private void Update()
        {
            if (_isActiveGame == false)
            {
                return;
            }

            float direction = _joystick.Horizontal;

            Movement(direction);
        }

        #region Private Methods

        private void Movement(float direction)
        {
            if(_constMovingTime <= 0)
            {
                _movingSpeed -= Time.deltaTime / _subtractinSpeedFromTime;
            }
            else if (_constMovingTime > 0)
            {
                _constMovingTime -= Time.deltaTime;

                if (_slowerMovingTime < 0)
                {
                    return;
                }

                _slowerMovingTime -= Time.deltaTime;
            }

            if (_movingSpeed < 0)
            {
                _isActiveGame = false;
    
                OnRunOutTime?.Invoke();

                return;
            }

            _transform.localRotation = Quaternion.Euler(0f, direction * FORCE_ROTATE, 0f);
            _transform.Translate(new Vector3(0f, 0f, _movingSpeed * Time.deltaTime));
        }

        #endregion
    }
}