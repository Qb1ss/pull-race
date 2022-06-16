using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using Configs;
using Location;
using Obstructions;
using Interface.Upgrades;

namespace Character
{
    [RequireComponent(typeof(Rigidbody), typeof(Transform))]
    public class Character_Movement : MonoBehaviour
    {
        #region EVENTS

        public static UnityEvent<int, int> OnRunOutTime = new UnityEvent<int, int>();
        public static UnityEvent<int, int> OnLoseRunOutTime = new UnityEvent<int, int>();
        public static UnityEvent<float> OnStartedGame = new UnityEvent<float>();

        public static UnityEvent OnLoseLevel = new UnityEvent();
        public static UnityEvent OnWinLevel = new UnityEvent();
        public static UnityEvent OnCrash = new UnityEvent();

        #endregion

        #region CONSTS

        private const float FORCE_ROTATE = 30f;
        private const float DAMAGE = 1f;
        private const float X_POSITION = 8.5f;

        private const string TAG_FINISH = "Finish";
        private const string TAG_RESPAWN = "Respawn";
        private const string TAG_EDITOR_ONLY = "EditorOnly";

        #endregion

        [Header("Parameters")]
        [SerializeField] private CharacterParametersConfig _parameters;
        [SerializeField] private Joystick _joystick;

        [HideInInspector] public float MovingSpeed;

        private float _constMovingTime;
        private float _constMovementTime;
        private float _forceTensionSlingshot;
        private float _slowerMovingTime;
        private float _subtractinSpeedFromTime;
        private float _maxCarForce;
        private float _carForce;

        private int _startZPosition;

        private bool _isActiveGame = false;
        private bool _isKissTheWall = false;

        private Transform _transform;
        private Rigidbody _rigidbody;

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

            MovingSpeed = _movementSpeed * forceTension;
            _subtractinSpeedFromTime = _slowerMovingTime / MovingSpeed;

            UpdateParameters();

            OnStartedGame?.Invoke(_constMovementTime);
        }


        private void Movement()
        {
            float direction = _joystick.Horizontal;
            float divTime = 1;

            if (_transform.position.x >= X_POSITION || _transform.position.x <= -X_POSITION)
            {
                direction = 0;               
            }

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

            if (MovingSpeed < 0)
            {
                EndGame();

                return;
            }

            _transform.localRotation = Quaternion.Euler(0f, direction * FORCE_ROTATE, 0f);

            if(_isKissTheWall == true)
            {
                direction = 0f;
            }

            _transform.position += new Vector3(direction * divTime, 0f, MovingSpeed * Time.deltaTime);
        }


        private void CrashInObject(Obstruction obstruction)
        {
            _carForce -= DAMAGE;

            if (_carForce <= 0)
            {
                ParticleSystem effect = Instantiate(_destroyEffect, _transform.position, Quaternion.identity);
                Destroy(effect, 1f);

                Handheld.Vibrate();

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

            if (collision.gameObject.CompareTag(TAG_RESPAWN))
            {
                EndGame();
            }

            if (collision.gameObject.CompareTag(TAG_EDITOR_ONLY))
            {
                _isKissTheWall = true;
            }
        }


        private void OnCollisionExit(Collision collision)
        {
            if (collision.gameObject.CompareTag(TAG_EDITOR_ONLY))
            {
                _isKissTheWall = false;
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