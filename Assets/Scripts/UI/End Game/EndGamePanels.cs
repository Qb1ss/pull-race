using UnityEngine;
using UnityEngine.Events;
using Character;

namespace Interface.EndGame
{
    public class EndGamePanels : MonoBehaviour
    {
        #region EVENTS

        public static UnityEvent<int> OnLoseLevel = new UnityEvent<int>();

        #endregion

        [Header("Panels")]
        [SerializeField] private WinGamePanel _winGamePanel = null;
        [SerializeField] private LoseGamePanel _loseGamePanel = null;

        private int _value = 0;


        #region MONO

        private void Start()
        {
            _winGamePanel.gameObject.SetActive(false);
            _loseGamePanel.gameObject.SetActive(false);
        }


        private void OnEnable()
        {
            Character_Movement.OnWinLevel.AddListener(() => _winGamePanel.gameObject.SetActive(true));
            Character_Movement.OnLoseLevel.AddListener(() => ActivatingLoseLevelPanel());

            CoinsDisplay.OnGetCoin.AddListener(GetCoin);
        }

        #endregion

        private void GetCoin(int value)
        {
            _value = value;
        }


        private void ActivatingLoseLevelPanel()
        {
            _loseGamePanel.gameObject.SetActive(true);
            
            OnLoseLevel?.Invoke(_value);
        }
    }
}