using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Scenes;
using Location;

namespace Interface.CreativeMenu
{
    public class CreativeMenu_Settings : MonoBehaviour
    {
        #region CONSTS

        private const string COINS_PLAYER_PREFS = "CoinsPlayerPrefs";
        private const string LOCATION_TYPE_PLAYER_PREFS = "LocationTypePlayerPrefs";
        private const string SKINS_PLAYER_PREFS = "SkinsPlayerPrefs";

        #endregion

        [Header("Colors")]
        [SerializeField] private Color _onColor;
        [SerializeField] private Color _offColor;

        [Header("Trancition")]
        [SerializeField] private Button _trancitionButton = null;

        [Header("Coins")]
        [SerializeField] private Button _coinsButton = null;
        [SerializeField] private TextMeshProUGUI _coinsStatusText = null;

        private bool _coinStatus = false;

        [Header("Location Type")]
        [SerializeField] private Button[] _locationTypeButtons;

        [Header("Skins")]
        [SerializeField] private Button[] _skinsButtons;

        private SceneTrancition _sceneTrancition = null;


        #region MONO

        private void Awake()
        {
            _sceneTrancition = new SceneTrancition();
        }

        private void Start()
        {
            _coinsButton.onClick.AddListener(() => ChangedCoinStatus());

            _trancitionButton.onClick.AddListener(() => _sceneTrancition.OnTrancitionToNextScene());

            _locationTypeButtons[0].onClick.AddListener(() => ChangedLocationTypeStatus(0));
            _locationTypeButtons[1].onClick.AddListener(() => ChangedLocationTypeStatus(1));
            _locationTypeButtons[2].onClick.AddListener(() => ChangedLocationTypeStatus(2));

            _skinsButtons[0].onClick.AddListener(() => ChangedSkinStatus(0));
            _skinsButtons[1].onClick.AddListener(() => ChangedSkinStatus(1));
            _skinsButtons[2].onClick.AddListener(() => ChangedSkinStatus(2));
            _skinsButtons[3].onClick.AddListener(() => ChangedSkinStatus(3));
            _skinsButtons[4].onClick.AddListener(() => ChangedSkinStatus(4));
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

        private void ChangedLocationTypeStatus(int typeIndex)
        {
            PlayerPrefs.SetInt(LOCATION_TYPE_PLAYER_PREFS, typeIndex);

            foreach(Button image in _locationTypeButtons)
            {
                image.GetComponent<Image>().color = _offColor;
            }

            _locationTypeButtons[typeIndex].GetComponent<Image>().color = _onColor;
        }

        private void ChangedSkinStatus(int skinIndex)
        {
            PlayerPrefs.SetInt(SKINS_PLAYER_PREFS, skinIndex);

            foreach (Button image in _skinsButtons)
            {
                image.GetComponent<Image>().color = _offColor;
            }

            _locationTypeButtons[skinIndex].GetComponent<Image>().color = _onColor;
        }

        #endregion
    }
}