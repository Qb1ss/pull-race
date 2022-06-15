using UnityEngine;
using UnityEngine.Events;
using System;

namespace WalletData
{
    public class Wallet
    {
        #region EVENTS

        public static Action<Currency, int> OnCurrencyIncreased;
        public static Action<Currency, int> OnCurrencyDecreased;

        public static UnityEvent<Currency, int> OnUpdateDisplay = new UnityEvent<Currency, int>();

        #endregion

        #region Public Methods

        public int GetCount(Currency currency)
        {
            return PlayerPrefs.GetInt(currency.ToString(), 0);
        }


        public void Increase(Currency currency, int value)
        {
            ChangeCurrency(currency, value);

            OnCurrencyIncreased?.Invoke(currency, value);
        }


        public void Decrease(Currency currency, int value)
        {
            ChangeCurrency(currency, -value);

            OnCurrencyDecreased?.Invoke(currency, value);
        }

        #endregion

        #region Private Methods

        private void ChangeCurrency(Currency currency, int value)
        {
            int current = GetCount(currency);

            SetCount(currency, current + value);
        }


        private void SetCount(Currency currency, int value)
        {
            PlayerPrefs.SetInt(currency.ToString(), value);

            OnUpdateDisplay?.Invoke(currency, value);
        }

        #endregion
    }
}