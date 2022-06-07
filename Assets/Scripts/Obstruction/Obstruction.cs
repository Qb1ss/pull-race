using UnityEngine;
using Configs;

namespace Obstructions
{
    public enum ObstructionTypes
    {
        Block = 0,
        Car = 1
    }

    [RequireComponent(typeof(BoxCollider))]
    public class Obstruction : MonoBehaviour
    {
        [SerializeField] private ObstructionConfig _parameters;

        #region CONSTS

        private const string TAG_FINISH = "Finish";

        private const float MIN_FACTOR_SPEED = 0.8f;
        private const float MAX_FACTOR_SPEED = 2f;

        #endregion

        [Header("Main Parameters")]
        [SerializeField] private ObstructionTypes _obstructionTypes;

        private float _movingSpeed;

        private bool _isGameActive = false;

        private BoxCollider _boxCollider;

        #region Private Fields

        private float _movementSpeed => _parameters.MovementSpeed;

        #endregion


        #region MONO

        private void OnEnable()
        {
            _boxCollider = GetComponent<BoxCollider>();

            DynamicJoystick.OnStartGame.AddListener(OnStartGame);
        }

        #endregion

        private void Update()
        {
            if(_isGameActive == false)
            {
                return;
            }

            Car_Movement();
        }

        #region Public Methods

        public void OnDestroing()
        {
            _boxCollider.enabled = false;

            //ύττεκς

            Destroy(gameObject);
        }

        #endregion

        #region Private Methods

        #region Car

        private void Car_Movement()
        {
            if (_obstructionTypes != ObstructionTypes.Car)
            {
                return;
            }

            gameObject.transform.position += Vector3.forward * _movingSpeed * Time.deltaTime;
        }

        #endregion

        private void OnStartGame(float forceTension)
        {
            _isGameActive = true;

            _movingSpeed = _movementSpeed * Random.Range(MIN_FACTOR_SPEED, MAX_FACTOR_SPEED);
        }

        #endregion

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(TAG_FINISH))
            {
                Destroy(gameObject);
            }
        }
    }
}