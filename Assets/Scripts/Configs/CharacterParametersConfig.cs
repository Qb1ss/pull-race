using UnityEngine;
using Interface.Upgrades;

namespace Configs
{
    [CreateAssetMenu(fileName = "Character Level ", menuName = "Configs/Characters")]
    public class CharacterParametersConfig : ScriptableObject
    {
        #region CONSTS

        private const string TENSION_SLINGSHOT_PLAYER_PREFS = "TensionSlingshotPlayerPrefs";
        private const string FORCE_PLAYER_PREFS = "ForcePlayerPrefs";
        private const string MOVING_TIME_PLAYER_PREFS = "MovingTimePlayerPrefs";

        private const float DEFAULT_TENSION_VALUE = 1f;
        private const float DEFAULT_FORCE_VALUE = 1f;
        private const float DEFAULT_MOVING_TIME_VALUE = 20f;

        #endregion

        [Header("Character Parameters")]
        [SerializeField] private float _movementSpeed;
        [Space(height: 5f)]

        [SerializeField] private float _multiplicationFactorSecMovementTimer = 2f;
        private float _constMovementTimer;
        [SerializeField] private float _slowerMovementTimer;
        [Space(height: 5f)]

        [SerializeField] private float _multiplicationFactorForceTensionSlingshot = 0.5f;
        private float _forceTensionSlingshot = 1;
        [Space(height: 5f)]

        [SerializeField] private float _multiplicationFactorMaxCarForce = 0.5f;
        private float _maxCarForce = 1;

        #region Public Fields

        public float MovementSpeed => _movementSpeed;
        public float ConstMovementTimer => _constMovementTimer;
        public float SlowerMovementTimer => _slowerMovementTimer;
        public float ForceTensionSlingshot => _forceTensionSlingshot;
        public float MaxCarForce => _maxCarForce;

        #endregion


        #region MONO

        private void OnEnable()
        {
            StartUpdateParameters();

            UpgradeButton.OnUpgradeParameter.AddListener(OnUpgradeParameter);
        }

        #endregion

        #region Private Methods

        private void StartUpdateParameters()
        {
            _forceTensionSlingshot = PlayerPrefs.GetFloat(TENSION_SLINGSHOT_PLAYER_PREFS, DEFAULT_TENSION_VALUE);
            _constMovementTimer = PlayerPrefs.GetFloat(MOVING_TIME_PLAYER_PREFS, DEFAULT_MOVING_TIME_VALUE);
            _maxCarForce = PlayerPrefs.GetFloat(FORCE_PLAYER_PREFS, DEFAULT_FORCE_VALUE);

            Debug.Log($"force_tension_slingshot: {_forceTensionSlingshot} | force: {_maxCarForce} | moving_time: {_constMovementTimer}");
        }


        private void OnUpgradeParameter(TypeUpgrades upgrades)
        {
            if(upgrades == TypeUpgrades.Slingshot)
            {
                _forceTensionSlingshot = _forceTensionSlingshot + _multiplicationFactorForceTensionSlingshot;
                PlayerPrefs.SetFloat(TENSION_SLINGSHOT_PLAYER_PREFS, _forceTensionSlingshot);

                Debug.Log($"New Tension: {_forceTensionSlingshot}");
            }
            else if (upgrades == TypeUpgrades.Force)
            {
                _maxCarForce = _maxCarForce + _multiplicationFactorMaxCarForce;
                PlayerPrefs.SetFloat(FORCE_PLAYER_PREFS, _maxCarForce);

                Debug.Log($"New Force: {_maxCarForce}");
            }
            else if (upgrades == TypeUpgrades.MovingTime)
            {
                _constMovementTimer = _constMovementTimer + _multiplicationFactorSecMovementTimer;
                PlayerPrefs.SetFloat(MOVING_TIME_PLAYER_PREFS, _constMovementTimer);

                Debug.Log($"New Time: {_constMovementTimer}");
            }
        }

        #endregion
    }
}