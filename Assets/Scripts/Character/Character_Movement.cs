using UnityEngine;
using UnityEngine.Events;
using Configs;
using Obstructions;
using Interface.Upgrades;

namespace Character
{
    [RequireComponent(typeof(Rigidbody), typeof(Transform))]
    public class Character_Movement : MonoBehaviour
    {
        #region EVENTS

        public static UnityEvent<int , int> OnRunOutTime = new UnityEvent<int, int>();

        public static UnityEvent OnLoseLevel = new UnityEvent();
        public static UnityEvent OnWinLevel = new UnityEvent();
        public static UnityEvent OnCrash = new UnityEvent();

        #endregion

        #region CONSTS

        private const float FORCE_ROTATE = 20f;
        private const float DAMAGE = 1f;

        private const string TAG_FINISH = "Finish";

        #endregion

        [SerializeField] private CharacterParametersConfig _parameters;
        [SerializeField] private Joystick _joystick;

        private float _movingSpeed;
        private float _constMovingTime;
        private float _constMovementTime;
        private float _forceTensionSlingshot;
        private float _slowerMovingTime;
        private float _subtractinSpeedFromTime;
        private float _maxCarForce;
        private float _carForce;

        private int _startZPosition;

        private bool _isActiveGame = false;

        private Transform _transform;
        private Rigidbody _rigidbody;

        #region Private Fields

        private float _movementSpeed => _parameters.MovementSpeed;
        private float _slowerMovementTime => _parameters.SlowerMovementTimer;

        #endregion


        #region MONO

        private void Awake()
        {
            _transform = GetComponent<Transform>();
            _rigidbody = GetComponent<Rigidbody>();
        }


        private void Start()
        {
            _startZPosition = (int)_transform.position.z;

            UpdateParameters();
        }


        private void OnEnable()
        {
            DynamicJoystick.OnStartGame.AddListener(OnStartGame);
            UpgradesButtons.OnStartGame.AddListener(UpdateParameters);
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

        private void UpdateParameters()
        {
            _constMovementTime = _parameters.ConstMovementTimer;
            _constMovingTime = _constMovementTime;
            _forceTensionSlingshot = _parameters.ForceTensionSlingshot;

            _constMovingTime = _constMovementTime;
            _slowerMovingTime = _constMovementTime / 10;
            _maxCarForce = _parameters.MaxCarForce;
            _carForce = _maxCarForce;
        }


        private void OnStartGame(float forceTension)
        {
            _isActiveGame = true;

            _rigidbody.isKinematic = false;

            _movingSpeed = _movementSpeed * forceTension;
            _subtractinSpeedFromTime = _slowerMovingTime / _movingSpeed;

            UpdateParameters();
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
                EndGame();

                return;
            }

            _transform.localRotation = Quaternion.Euler(0f, direction * FORCE_ROTATE, 0f);
            _transform.Translate(new Vector3(0f, 0f, _movingSpeed * Time.deltaTime));
        }


        private void CrashInObject(Obstruction obstruction)
        {
            _carForce -= DAMAGE;

            if (_carForce <= 0)
            {
                EndGame();
            }
            else
            {
                obstruction.OnDestroing();

                OnCrash?.Invoke();
            }
        }


        private void EndGame()
        {
            _isActiveGame = false;

            OnRunOutTime?.Invoke(_startZPosition, (int)_transform.position.z);
            OnLoseLevel?.Invoke();
        }


        private void WinGame()
        {
            _isActiveGame = false;

            OnRunOutTime?.Invoke(_startZPosition, (int)_transform.position.z);
            OnWinLevel?.Invoke();
        }

        #endregion

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent<Obstruction>(out Obstruction obstruction))
            {
                CrashInObject(obstruction);
            }
        }


        private void OnTriggerExit(Collider collider)
        {
            if (collider.gameObject.CompareTag(TAG_FINISH))
            {
                WinGame();
            }
        }
    }
}