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

        public static UnityEvent<int, int> OnRunOutTime = new UnityEvent<int, int>();
        public static UnityEvent<float> OnStartedGame = new UnityEvent<float>();

        public static UnityEvent OnLoseLevel = new UnityEvent();
        public static UnityEvent OnWinLevel = new UnityEvent();
        public static UnityEvent OnCrash = new UnityEvent();

        #endregion

        #region CONSTS

        private const float FORCE_ROTATE = 25f;
        private const float DAMAGE = 1f;
        private const float X_POSITION = 7.25f;

        private const string TAG_FINISH = "Finish";

        private const string NAME_JOYSTICK = "Dynamic Joystick";

        #endregion

        [Header("Parameters")]
        [SerializeField] private CharacterParametersConfig _parameters;
        private Joystick _joystick;

        [HideInInspector] public float MovingSpeed;

        private float _constMovingTime;
        private float _constMovementTime;
        private float _slowerMovingTime;
        private float _subtractinSpeedFromTime;
        private float _maxCarForce;
        private float _carForce;

        private int _startZPosition;

        private bool _isActiveGame = false;

        private Transform _transform;

        #region Private Fields

        private float _movementSpeed => _parameters.MovementSpeed;
        private float _slowerMovementTime => _parameters.SlowerMovementTimer;

        private ParticleSystem _destroyEffect => _parameters.DestroyEffect;

        #endregion


        #region MONO

        private void Awake()
        {
            _transform = GetComponent<Transform>();

            _joystick = GameObject.Find(NAME_JOYSTICK).GetComponent<Joystick>();
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
            float direction = _joystick.Horizontal;
            float divTime = 1;

            #region Location Border

            if (_transform.position.x >= X_POSITION || _transform.position.x <= -X_POSITION)
            {
                direction = 0;

                if (_joystick.Horizontal < 0 && _transform.position.x >= X_POSITION)
                {
                    direction = _joystick.Horizontal;
                }
                else if (_joystick.Horizontal > 0 && _transform.position.x <= -X_POSITION)
                {
                    direction = _joystick.Horizontal;
                }

                _transform.localRotation = Quaternion.Euler(0f, direction * FORCE_ROTATE, 0f);
                _transform.position += new Vector3(direction * divTime, 0f, MovingSpeed * Time.deltaTime);

                return;
            }

            #endregion

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

            _transform.localRotation = Quaternion.Euler(0f, direction * FORCE_ROTATE, 0f);
            _transform.position += new Vector3(direction * divTime, 0f, MovingSpeed * Time.deltaTime);
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

            ParticleSystem effect = Instantiate(_destroyEffect, _transform.position, Quaternion.identity);
            Destroy(effect, 1f);

            OnRunOutTime?.Invoke(_startZPosition, (int)_transform.position.z);
            OnLoseLevel?.Invoke();

            Handheld.Vibrate();
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