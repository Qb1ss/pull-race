using UnityEngine;
using TMPro;
using Character;
using WalletData;
using Interface.EndGame;

namespace Interface
{
    public class CoinsDisplay : MonoBehaviour
    {
        private Wallet _wallet;

        private TextMeshProUGUI _coinsDisplay;

        private int _coins = 0; 
        private int _coinValue = 0; 
        private int _coinDivision = 10;


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
            WinGamePanel.OnGetCoins.AddListener(UpdateCoins); 
            Character_Movement.OnRunOutTime.AddListener(RunOutTime);
            Character_Movement.OnLoseRunOutTime.AddListener(LoseRunOutTime);
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
            _coinValue = (int)(endValue - startValue) / _coinDivision;
        }


        private void LoseRunOutTime(int startValue, int endValue)
        {
            int value = (int)(endValue - startValue) / _coinDivision;

            _wallet.Increase(Currency.Coin, value);
            UpdateCoinsDisplay(Currency.Coin, value);
        }


        private void UpdateCoins(int value)
        {
            value = _coinValue;

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