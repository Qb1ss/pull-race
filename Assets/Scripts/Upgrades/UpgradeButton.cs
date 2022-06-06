using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using Configs;

namespace Interface.Upgrades
{
    public enum TypeUpgrades
    {
        Slingshot = 0,
        Force = 1,
        MovingTime = 2
    }

    public class UpgradeButton : MonoBehaviour
    {
        #region EVENTS

        public static UnityEvent<TypeUpgrades> OnUpgradeParameter = new UnityEvent<TypeUpgrades>();

        #endregion

        #region CONSTS

        private const string PRICE_PLAYER_PREFS = "PricePlayerPrefs";

        #endregion

        [SerializeField] private UpgradesConfigs _parameters;

        [Header("Parameters")]
        [SerializeField] private TypeUpgrades _typeUpgrades;


        [Header("Components")]
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private TextMeshProUGUI _priceText;
        private Button _upgradeButton;

        private int _upgradePrice;

        #region Public Fields



        #endregion

        #region Private Fields

        private int _typeUpgradeIndex => (int)_typeUpgrades;

        private string _nameButton => _parameters.NamesButton[_typeUpgradeIndex];
        private int _startPrice => _parameters.StartPricesButton[_typeUpgradeIndex];
        private float _multiplicationFactorPrice => _parameters.MultiplicationFactorPrice;

        #endregion


        #region MONO

        private void Awake()
        {
            _upgradeButton = GetComponent<Button>();
        }

        private void Start()
        {
            UpdateParameters();

            _upgradeButton.onClick.AddListener(() => OnUpgrade());
        }

        #endregion

        #region Private Methods

        private void UpdateParameters()
        {
            _nameText.text = _nameButton;

            _upgradePrice = PlayerPrefs.GetInt($"{PRICE_PLAYER_PREFS}{_typeUpgradeIndex}", _startPrice);

            _priceText.text = _upgradePrice.ToString();
        }


        private void OnUpgrade()
        {
            int newValue = Mathf.RoundToInt(_upgradePrice * _multiplicationFactorPrice);

            _upgradePrice = newValue;
            PlayerPrefs.SetInt($"{PRICE_PLAYER_PREFS}{_typeUpgradeIndex}", _upgradePrice);

            _priceText.text = _upgradePrice.ToString();

            OnUpgradeParameter?.Invoke(_typeUpgrades);

            //аналитика на частоту прокачки
        }

        #endregion
    }
}