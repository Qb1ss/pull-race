using UnityEngine;
using UnityEngine.Events;
using Configs;
using Obstructions;
using Interface.Upgrades;
using MoreMountains.NiceVibrations;

namespace Character
{
    [RequireComponent(typeof(Rigidbody), typeof(Transform))]
    public class Character_Movement : MonoBehaviour
    {
        #region EVENTS

        public static UnityEvent<int, int> OnWinRunOutTime = new UnityEvent<int, int>();
        public static UnityEvent<int, int> OnLoseRunOutTime = new UnityEvent<int, int>();
        public static UnityEvent<float> OnStartedGame = new UnityEvent<float>();

        public static UnityEvent OnLoseLevel = new UnityEvent();
        public static UnityEvent OnWinLevel = new UnityEvent();
        public static UnityEvent OnCrash = new UnityEvent();

        #endregion

        #region CONSTS

        private const float FORCE_ROTATE = 25f;
        private const float DAMAGE = 1f;
        private const float X_POSITION = 8.45f;

        private const string TAG_FINISH = "Finish";
        private const string TAG_RESPAWN = "Respawn";
        private const string TAG_GAME_CONTROLLER = "GameController";

        #endregion

        [Header("Parameters")]
        [SerializeField] private CharacterParametersConfig _parameters;
        [SerializeField] private HapticTypes _hapticTypes = HapticTypes.HeavyImpact;
        private Transform _targetPosition;

        [HideInInspector] public float MovingSpeed;

        private float _constMovingTime;
        private float _constMovementTime;
        private float _slowerMovingTime;
        private float _subtractinSpeedFromTime;
        private float _maxCarForce;
        private float _carForce;
        private float _xPosition;

        private int _startZPosition;

        private bool _isActiveGame = false;

        private Transform _transform;
        private Rigidbody _rigidbody;
        private Camera _camera;

        #region Private Fields

        private float _movementSpeed => _parameters.MovementSpeed;
        private float _slowerMovementTime => _parameters.SlowerMovementTimer;

        private ParticleSystem _destroyEffect => _parameters.DestroyEffect;

        #endregion


        #region MONO

        private void Awake()
        {
            _transform = GetComponent<Transform>();
            _rigidbody = GetComponent<Rigidbody>();

            _targetPosition = GameObject.FindGameObjectWithTag(TAG_GAME_CONTROLLER).GetComponent<Transform>();
            _camera = Camera.main;
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
            _constMovementTime = _parameters.MovementTimer;
            _constMovingTime = _constMovementTime;

            _constMovingTime = _constMovementTime;
            _slowerMovingTime = _constMovementTime / 10;
            _maxCarForce = _parameters.MaxCarForce;
            _carForce = _maxCarForce;
        }


        private void OnStartGame(float forceTension)
        {
            _isActiveGame = true;

            MovingSpeed = _movementSpeed * forceTension;
            _subtractinSpeedFromTime = _slowerMovingTime / MovingSpeed;

            UpdateParameters();

            OnStartedGame?.Invoke(_constMovementTime);
        }


        private void Movement()
        {
            float divTime = 1;

            #region Timer

            if (_constMovingTime <= 0)
            {
                MovingSpeed -= Time.deltaTime / _subtractinSpeedFromTime;

                if (_slowerMovingTime < 0)
                {
                    return;
                }

                _slowerMovingTime -= Time.deltaTime;
                divTime -= Time.deltaTime;
            }
            else if (_constMovingTime > 0)
            {
                _constMovingTime -= Time.deltaTime;
            }

            #endregion

            if (MovingSpeed < 0)
            {
                EndGame();

                return;
            }

            #region Control

            Ray ray = _camera.ScreenPointToRay(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                _xPosition = hit.point.x;

                if (hit.point.x <= -X_POSITION)
                {
                    _xPosition = -X_POSITION;
                }
                else if (hit.point.x >= X_POSITION)
                {
                    _xPosition = X_POSITION;
                }
            }

            _targetPosition.position = new Vector3(_xPosition * divTime, _targetPosition.position.y, _targetPosition.position.z);
            _targetPosition.position += Vector3.forward * MovingSpeed * Time.deltaTime;

            #endregion

            _transform.LookAt(_targetPosition);
            _transform.Translate(new Vector3(0f, 0f, MovingSpeed * Time.deltaTime));
        }


        private void CrashInObject(Obstruction obstruction)
        {
            _carForce -= DAMAGE;

            if (_carForce <= 0)
            {
                ParticleSystem effect = Instantiate(_destroyEffect, _transform.position, Quaternion.identity);
                Destroy(effect, 1f);

                MMVibrationManager.Haptic(_hapticTypes, false, true, this);

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

            OnLoseRunOutTime?.Invoke(_startZPosition, (int)_transform.position.z);
            OnLoseLevel?.Invoke();
        }


        private void WinGame()
        {
            _isActiveGame = false;

            OnWinRunOutTime?.Invoke(_startZPosition, (int)_transform.position.z);
            OnWinLevel?.Invoke();
        }

        #endregion

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent<Obstruction>(out Obstruction obstruction))
            {
                CrashInObject(obstruction);
            }

            if (collision.gameObject.CompareTag(TAG_RESPAWN))
            {
                EndGame();
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