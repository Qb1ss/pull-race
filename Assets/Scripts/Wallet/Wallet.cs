using UnityEngine;
using UnityEngine.Events;
using System;

namespace WalletData
{
    public class Wallet
    {
        #region EVENTS

        public static Action<int> OnCurrencyIncreased;
        public static Action<int> OnCurrencyDecreased;

        public static UnityEvent<int> OnUpdateDisplay = new UnityEvent<int>();

        #endregion

        #region Public Methods

        public int GetCount()
        {
            return PlayerPrefs.GetInt("Coins", 0);
        }


        public void Increase(int value)
        {
            ChangeCurrency(value);

            OnCurrencyIncreased?.Invoke(value);
        }


        public void Decrease(int value)
        {
            ChangeCurrency(-value);

            OnCurrencyDecreased?.Invoke(value);
        }

        #endregion

        #region Private Methods

        private void ChangeCurrency(int value)
        {
            int current = GetCount();

            SetCount(current + value);
        }


        private void SetCount(int value)
        {
            PlayerPrefs.SetInt("Coins", value);

            OnUpdateDisplay?.Invoke(value);
        }

        #endregion
    }
}