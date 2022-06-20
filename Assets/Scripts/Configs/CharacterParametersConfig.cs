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
        private const string COINS_PLAYER_PREFS = "MovingTimePlayerPrefs";

        private const float DEFAULT_GAZ_VALUE = 8f;
        private const float DEFAULT_FORCE_VALUE = 1f;
        private const float DEFAULT_COIN_VALUE = 1f;

        #endregion

        [Header("Character Parameters")]
        [SerializeField] private float _movementSpeed;
        [SerializeField] private float _minMovementTimer = 10f;
        [Space(height: 5f)]

        [SerializeField] private ParticleSystem _destroyEffect;
        [Space(height: 5f)]

        [SerializeField] private float _multiplicationFactorSecMovementTimer = 2f;
        [SerializeField] private float _slowerMovementTimer;
        private float _constMovementTimer;
        [Space(height: 5f)]

        private float _forceTensionSlingshot = 1;
        [Space(height: 5f)]

        [SerializeField] private float _multiplicationFactorMaxCarForce = 0.5f;
        private float _maxCarForce = 1;
        [Space(height: 5f)]

        [SerializeField] private float _multiplicationFactorCoin = 1f;
        private float _multiplicationCoin = 1f;
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
            _multiplicationCoin = PlayerPrefs.GetFloat(COINS_PLAYER_PREFS, DEFAULT_COIN_VALUE);
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
                _multiplicationCoin = _multiplicationCoin + _multiplicationFactorCoin;
                PlayerPrefs.SetFloat(COINS_PLAYER_PREFS, _multiplicationCoin);
            }
        }

        #endregion
    }
}