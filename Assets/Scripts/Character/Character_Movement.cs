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

        private float _movingSpeed;
        private float _movingTime;
        private float _subtractinSpeedFromTime;

        private bool _isActiveGame = false;

        private Transform _transform;
        private Joystick _joystick;

        #region Private Fields

        private float _movementSpeed => _parameters.MovementSpeed;

        private float _movingTimer => _parameters.MovementTimer;

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
            _movingTime = _movingTimer;
            _subtractinSpeedFromTime = _movingTime / _movingSpeed;
        }


        private void OnEnable()
        {
            //пуск рогатки и начало движения
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
            if(_movingTime < 0)
            {
                return;
            }

            _movingTime -= Time.deltaTime;

            if (_movingSpeed < 0)
            {
                OnRunOutTime?.Invoke();

                return;
            }

            _movingSpeed -= Time.deltaTime / _subtractinSpeedFromTime;

            _transform.position += Vector3.forward * _movingSpeed * Time.deltaTime;
        }

        #endregion
    }
}