using UnityEngine;
using UnityEngine.Events;
using TMPro;
using Configs;
using Character;
using WalletData;
using Interface.EndGame;

namespace Interface
{
    public class CoinsDisplay : MonoBehaviour
    {
        #region EVENTS

        public static UnityEvent<int> OnGetCoin = new UnityEvent<int>();

        #endregion

        #region CONSTS

        private const int DIVISION_COIN = 10;

        #endregion

        [SerializeField] private CharacterParametersConfig _parameters = null;

        private Wallet _wallet = null;

        [SerializeField] private TextMeshProUGUI _coinsDisplay = null;

        private int _coins = 0; 
        private int _coinValue = 0;

        private bool _isStopedGame = false;

        #region Private Fields

        private int _multiplicationCoin => (int)_parameters.MultiplicationCoin;

        #endregion


        #region MONO

        private void Start()
        {
            _wallet = new Wallet();

            UpdateStartCoinsDisplay();

        }

        private void OnEnable()
        {
            Character_Movement.OnRunOutTime.AddListener(RunOutTime);

            WinGamePanel.OnGetCoins.AddListener(UpdateCoins);

            Wallet.OnUpdateDisplay.AddListener(UpdateCoinsDisplay);
        }

        private void OnDisable()
        {
            Character_Movement.OnRunOutTime.AddListener(RunOutTime);
        }

        #endregion

        #region Private Methods

        private void UpdateStartCoinsDisplay()
        {
            _coins = _wallet.GetCount();

            _coinsDisplay.text = _coins.ToString();
        }


        private void RunOutTime(int startValue, int endValue)
        {
            if (_isStopedGame == true) 
                return;
            else 
                _isStopedGame = true;

            int value = (int)(endValue - startValue) / DIVISION_COIN * _multiplicationCoin;

            OnGetCoin?.Invoke(value);

            _wallet.Increase(value);
            UpdateCoinsDisplay(value);
        }


        private void UpdateCoins(int value)
        {
            if (_isStopedGame == true) 
                return;
            else 
                _isStopedGame = true;

            value = _coinValue;

            _wallet.Increase(value);
            UpdateCoinsDisplay(value);
        }


        private void UpdateCoinsDisplay(int value)
        {
            _coins = _wallet.GetCount();

            _coinsDisplay.text = _coins.ToString();
        }

        #endregion
    }
}