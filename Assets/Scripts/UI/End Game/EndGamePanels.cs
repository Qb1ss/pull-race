using UnityEngine;
using UnityEngine.Events;
using Character;

namespace Interface.EndGame
{
    public class EndGamePanels : MonoBehaviour
    {
        public static UnityEvent<int> OnLoseLevel = new UnityEvent<int>();

        [Header("Panels")]
        [SerializeField] private WinGamePanel _winGamePanel;
        [SerializeField] private LoseGamePanel _loseGamePanel;

        private int _value;


        #region MONO

        private void Start()
        {
            _winGamePanel.gameObject.SetActive(false);
            _loseGamePanel.gameObject.SetActive(false);
        }


        private void OnEnable()
        {
            Character_Movement.OnLoseRunOutTime.AddListener(ActivatingLoseLevelPanel);
            Character_Movement.OnWinLevel.AddListener(() => _winGamePanel.gameObject.SetActive(true));
        }

        #endregion

        private void ActivatingLoseLevelPanel(int endValue, int startValue)
        {
            int value = (int)(endValue - startValue) / 10;

            _value = value;

            _loseGamePanel.gameObject.SetActive(true);

            OnLoseLevel?.Invoke(_value);
        }
    }
}