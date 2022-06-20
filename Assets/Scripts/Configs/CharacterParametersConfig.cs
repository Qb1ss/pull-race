using UnityEngine;
using Interface.Upgrades;

namespace Configs
{
    [CreateAssetMenu(fileName = "Character Level ", menuName = "Configs/Characters")]
    public class CharacterParametersConfig : ScriptableObject
    {
        #region CONSTS

        private const string GAZ_PLAYER_PREFS = "GazPlayerPrefs";
        private const string FORCE_PLAYER_PREFS = "ForcePlayerPrefs";
        private const string MOVING_TIME_PLAYER_PREFS = "MovingTimePlayerPrefs";

        private const float DEFAULT_GAZ_VALUE = 8f;
        private const float DEFAULT_FORCE_VALUE = 1f;

        #endregion

        [Header("Character Parameters")]
        [SerializeField] private float _movementSpeed;
        [SerializeField] private float _minMovementTimer = 10f;
        [Space(height: 5f)]

        [SerializeField] private ParticleSystem _destroyEffect;

        [Space(height: 5f)]
        [SerializeField] private float _multiplicationFactorSecMovementTimer = 2f;
        private float _constMovementTimer;
        [SerializeField] private float _slowerMovementTimer;
        [Space(height: 5f)]

        private float _forceTensionSlingshot = 1;
        [Space(height: 5f)]

        [SerializeField] private float _multiplicationFactorMaxCarForce = 0.5f;
        private float _maxCarForce = 1;
         [Space(height: 5f)]

        [SerializeField] private float _multiplicationFactorGaz = 1f;
        private float _multiplicationGaz = 1f;

        #region Public Fields

        public float MovementSpeed => _movementSpeed;
        public float ConstMovementTimer => _constMovementTimer;
        public float SlowerMovementTimer => _slowerMovementTimer;
        public float ForceTensionSlingshot => _forceTensionSlingshot;
        public float MaxCarForce => _maxCarForce;

        public ParticleSystem DestroyEffect => _destroyEffect;

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
            _constMovementTimer = PlayerPrefs.GetFloat(GAZ_PLAYER_PREFS, DEFAULT_GAZ_VALUE);
            _maxCarForce = PlayerPrefs.GetFloat(FORCE_PLAYER_PREFS, DEFAULT_FORCE_VALUE);
            _constMovementTimer = PlayerPrefs.GetFloat(MOVING_TIME_PLAYER_PREFS, _minMovementTimer);
        }


        private void OnUpgradeParameter(TypeUpgrades upgrades)
        {
            if(upgrades == TypeUpgrades.Gaz)
            {
                _constMovementTimer = _constMovementTimer + _multiplicationFactorGaz;
                PlayerPrefs.SetFloat(GAZ_PLAYER_PREFS, _forceTensionSlingshot);
            }
            else if (upgrades == TypeUpgrades.Force)
            {
                _maxCarForce = _maxCarForce + _multiplicationFactorMaxCarForce;
                PlayerPrefs.SetFloat(FORCE_PLAYER_PREFS, _maxCarForce);
            }
            else if (upgrades == TypeUpgrades.Coins)
            {
                _constMovementTimer = _constMovementTimer + _multiplicationFactorSecMovementTimer;
                PlayerPrefs.SetFloat(MOVING_TIME_PLAYER_PREFS, _constMovementTimer);
            }
        }

        #endregion
    }
}