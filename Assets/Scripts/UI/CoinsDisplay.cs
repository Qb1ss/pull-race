using UnityEngine;
using TMPro;
using WalletData;

namespace Interface
{
    public class CoinsDisplay : MonoBehaviour
    {
        private Wallet _wallet;

        private TextMeshProUGUI _coinsDisplay;

        private int _coins = 0;

        #region Private Fields

        private int _coinDivision = 10; // config

        #endregion


        #region MONO

        private void Awake()
        {
            _coinsDisplay = GetComponent<TextMeshProUGUI>();
        }


        private void Start()
        {
            _wallet = new Wallet();

            UpdateStartCoinsDisplay();
        }


        private void OnEnable()
        {
            Character.Character_Movement.OnRunOutTime.AddListener(RunOutTime);
            Wallet.OnUpdateDisplay.AddListener(UpdateCoinsDisplay);
        }


        private void OnDisable()
        {
            Wallet.OnUpdateDisplay.AddListener(UpdateCoinsDisplay);
        }

        #endregion

        #region PrivateMethods

        private void UpdateStartCoinsDisplay()
        {
            _coins = _wallet.GetCount(Currency.Coin);

            _coinsDisplay.text = _coins.ToString();
        }


        private void RunOutTime(int startValue, int endValue)
        {
            int value = (int)(endValue - startValue) / _coinDivision;

            _wallet.Increase(Currency.Coin, value);
            UpdateCoinsDisplay(Currency.Coin, value);
        }


        private void UpdateCoinsDisplay(Currency currency, int value)
        {
            _coins = _wallet.GetCount(Currency.Coin);

            _coinsDisplay.text = _coins.ToString();
        }

        #endregion
    }
}