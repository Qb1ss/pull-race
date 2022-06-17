using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using TMPro;
using DG.Tweening;
using Configs;
using Analytics;
using WalletData;

public enum TypeUpgrades
{
    Slingshot = 0,
    Force = 1,
    MovingTime = 2
}

namespace Interface.Upgrades
{
    public class UpgradeButton : MonoBehaviour
    {
        #region EVENTS

        public static UnityEvent<TypeUpgrades> OnUpgradeParameter = new UnityEvent<TypeUpgrades>();

        #endregion

        #region CONSTS

        private const string PRICE_PLAYER_PREFS = "PricePlayerPrefs";
        private const string LEVEL_PLAYER_PREFS = "LevelPlayerPrefs";

        private const float START_Y_POSITION = 100f;
        private const float FIRST_Y_POSITION = 200f;
        private const float SECOND_Y_POSITION = 300f;

        #endregion

        [SerializeField] private UpgradesConfigs _parameters;

        [Header("Parameters")]
        [SerializeField] private TypeUpgrades _typeUpgrades;

        [Header("Effect")]
        [SerializeField] private RectTransform _effect;
        [SerializeField] private Color _colorEffect;
        [Space(height: 5f)]

        [SerializeField] private float _timeAnimation = 0.5f;

        [Header("Components")]
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private TextMeshProUGUI _priceText;
        [SerializeField] private TextMeshProUGUI _levelText;
        [SerializeField] private Image _icon;
        [Space(height: 5f)]

        private Button _upgradeButton;

        private int _upgradePrice;
        private int _upgradeLevel = 1;

        private Wallet _wallet;

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
            _wallet = new Wallet();

            UpdateParameters();

            _upgradeButton.onClick.AddListener(() => OnUpgrading());
        }

        #endregion

        #region Private Methods

        private void UpdateParameters()
        {
            _nameText.text = _nameButton;

            _icon.sprite = _parameters.IconUpgrades[_typeUpgradeIndex];

            _upgradePrice = PlayerPrefs.GetInt($"{PRICE_PLAYER_PREFS}{_typeUpgradeIndex}", _startPrice);
            _upgradeLevel = PlayerPrefs.GetInt($"{LEVEL_PLAYER_PREFS}{_typeUpgradeIndex}", _upgradeLevel);

            _priceText.text = _upgradePrice.ToString();
            _levelText.text = $"Level{_upgradeLevel}";

            _effect.anchoredPosition = new Vector2(_effect.anchoredPosition.x, START_Y_POSITION);
            _effect.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
        }


        private void OnUpgrading()
        {
            if(_wallet.GetCount(Currency.Coin) >= _upgradePrice)
            {
                _wallet.Decrease(Currency.Coin, _upgradePrice);

                OnUpgrade();
            }
        }


        private void OnUpgrade()
        {
            int newValue = Mathf.RoundToInt(_upgradePrice * _multiplicationFactorPrice);

            _upgradePrice = newValue;
            PlayerPrefs.SetInt($"{PRICE_PLAYER_PREFS}{_typeUpgradeIndex}", _upgradePrice);

            _upgradeLevel++;
            PlayerPrefs.SetInt($"{LEVEL_PLAYER_PREFS}{_typeUpgradeIndex}", _upgradeLevel);

            _priceText.text = _upgradePrice.ToString();
            _levelText.text = $"Level{_upgradeLevel}";

            StartCoroutine(AnimationCoroutine());

            OnUpgradeParameter?.Invoke(_typeUpgrades);

            Analytics_GameAnalytics.OnUpgradeParameter(_typeUpgrades);
            Analytics_Facebook.OnUpgradeParameter(_typeUpgrades);
        }

        #endregion

        private IEnumerator AnimationCoroutine()
        {
            _effect.anchoredPosition = new Vector2(_effect.anchoredPosition.x, START_Y_POSITION);
            _effect.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);

            _effect.DOLocalMoveY(FIRST_Y_POSITION, _timeAnimation);
            _effect.GetComponent<Image>().DOColor(_colorEffect, _timeAnimation);

            yield return new WaitForSeconds(_timeAnimation);

            _effect.DOLocalMoveY(SECOND_Y_POSITION, _timeAnimation);
            _effect.GetComponent<Image>().DOColor(new Color(0f, 0f, 0f, 0f), _timeAnimation);

            yield break;
        }
    }
}