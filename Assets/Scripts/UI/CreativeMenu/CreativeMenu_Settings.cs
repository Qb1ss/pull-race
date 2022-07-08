using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Location;

namespace Interface.CreativeMenu
{
    public class CreativeMenu_Settings : MonoBehaviour
    {
        #region CONSTS

        private const string COINS_PLAYER_PREFS = "CoinsPlayerPrefs";
        private const string LOCATION_TYPE_PLAYER_PREFS = "LocationTypePlayerPrefs";

        #endregion

        [Header("Coins")]
        [SerializeField] private Button _coinsButton = null;
        [SerializeField] private TextMeshProUGUI _coinsStatusText = null;

        [SerializeField] private Color _onColor;
        [SerializeField] private Color _offColor;

        private bool _coinStatus = false;


        #region MONO

        private void Start()
        {
            _coinsButton.onClick.AddListener(() => ChangedCoinStatus());
        }

        #endregion

        #region Private Methods

        private void ChangedCoinStatus()
        {
            _coinStatus = !_coinStatus;

            if(_coinStatus == true)
            {
                PlayerPrefs.SetInt(COINS_PLAYER_PREFS, 1);

                _coinsButton.GetComponent<Image>().color = _onColor;

                _coinsStatusText.text = "On";
            }
            else if (_coinStatus == false)
            {
                PlayerPrefs.SetInt(COINS_PLAYER_PREFS, 0);

                _coinsButton.GetComponent<Image>().color = _offColor;

                _coinsStatusText.text = "Off";
            }
        }

        #endregion
    }
}