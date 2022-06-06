using UnityEngine;

namespace Obstructions
{
    public enum ObstructionTypes
    {
        Block = 0,
        Car = 1
    }

    public class Obstruction : MonoBehaviour
    {
        [Header("Main Parameters")]
        [SerializeField] private ObstructionTypes _obstructionTypes;

        private bool _isGameActive = false;

        #region Private Fields

        private float _movingSpeed = 10; //config

        #endregion


        #region MONO

        private void OnEnable()
        {
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
        }

        #endregion
    }
}