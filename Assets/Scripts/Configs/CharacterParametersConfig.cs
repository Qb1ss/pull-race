using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "Character Level ", menuName = "Configs/Characters")]
    public class CharacterParametersConfig : ScriptableObject
    {
        #region CONSTS

        private const string GAZ_PLAYER_PREFS = "GazPlayerPrefs";
        private const string FORCE_PLAYER_PREFS = "ForcePlayerPrefs";
        private const string COINS_PLAYER_PREFS = "MovingTimePlayerPrefs";

        private const float DEFAULT_GAZ_VALUE = 12f;
        private const float DEFAULT_FORCE_VALUE = 1f;
        private const float DEFAULT_COIN_VALUE = 1f;

        #endregion

        #region Inspector

        [Header("Character Parameters")]
        [Tooltip("Эффект взрыва")]
        [SerializeField] private ParticleSystem _destroyEffect = null;
        
        [Header("Speed Parameters")]
        [Tooltip("Скорость передвижения")]
        [SerializeField] private float _movementSpeed = 50f;

        [Header("Time (Gaz) Parameters")]
        [Tooltip("Множитель основного времени движения\n во сколько")]
        [SerializeField] private float multiplicationFactorSecTime = 1.2f;

        [Tooltip("Время обычного движения")]
        [SerializeField] private float _movementTimer = 12f;
        [Space(height: 5f)]

        [Tooltip("Время замедленного движения")]
        [SerializeField] private float _slowerMovementTimer = 5f;

        [Header("Force Parameters")]
        [Tooltip("Множитель здоровья автомобиля\n на сколько")]
        [SerializeField] private float _multiplicationFactorMaxCarForce = 0.5f;

        [Tooltip("Здоровье автомобиля")]
        [SerializeField] private float _maxCarForce = 1f;

        [Header("Coin Parameters")]
        [Tooltip("Множитель монет\n на сколько")]
        [SerializeField] private float _multiplicationFactorCoin = 1f;

        [Tooltip("Множитель монет")]
        [SerializeField] private float _multiplicationCoin = 1f;

        #endregion

        #region Public Fields

        public ParticleSystem DestroyEffect => _destroyEffect;

        public float MovementSpeed => _movementSpeed;

        public float MovementTimer => _movementTimer;
        public float SlowerMovementTimer => _slowerMovementTimer;

        public float MaxCarForce => _maxCarForce;

        public float MultiplicationCoin => _multiplicationCoin;

        #endregion


        #region MONO

        private void OnEnable()
        {
            StartUpdateParameters();

            Interface.Upgrades.UpgradeButton.OnUpgradeParameter.AddListener(OnUpgradeParameter);
        }

        #endregion

        #region Private Methods

        private void StartUpdateParameters()
        {
            _movementTimer = PlayerPrefs.GetFloat(GAZ_PLAYER_PREFS, DEFAULT_GAZ_VALUE);
            _maxCarForce = PlayerPrefs.GetFloat(FORCE_PLAYER_PREFS, DEFAULT_FORCE_VALUE);
            _multiplicationCoin = PlayerPrefs.GetFloat(COINS_PLAYER_PREFS, DEFAULT_COIN_VALUE);
        }


        private void OnUpgradeParameter(TypeUpgrades upgrades)
        {
            if(upgrades == TypeUpgrades.Gaz)
            {
                _movementTimer = _movementTimer * multiplicationFactorSecTime;
                PlayerPrefs.SetFloat(GAZ_PLAYER_PREFS, _movementTimer);
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