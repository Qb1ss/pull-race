using UnityEngine;
using TMPro;

namespace Interface
{
    public class CoinsDisplay : MonoBehaviour
    {
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
            UpdateStartCoinsDisplay();
        }


        private void OnEnable()
        {
            Character.Character_Movement.OnRunOutTime.AddListener(RunOutTime);
        }

        #endregion

        #region PrivateMethods

        private void UpdateStartCoinsDisplay()
        {
            _coinsDisplay.text = _coins.ToString();
        }


        private void RunOutTime(int startValue, int endValue)
        {
            Debug.Log($"Start Value: {startValue} | End Value: {endValue}");

            int value = (int)(endValue - startValue) / _coinDivision;

            UpdateCoinsDisplay(value);
        }


        private void UpdateCoinsDisplay(int value)
        {
            _coins += value;

            _coinsDisplay.text = _coins.ToString();
        }

        #endregion
    }
}