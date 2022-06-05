using UnityEngine;
using Character;

namespace Interface.EndGame
{
    public class EndGamePanels : MonoBehaviour
    {
        [Header("Panels")]
        [SerializeField] private WinGamePanel _winGamePanel;
        [SerializeField] private LoseGamePanel _loseGamePanel;


        #region MONO

        private void Start()
        {
            _winGamePanel.gameObject.SetActive(false);
            _loseGamePanel.gameObject.SetActive(false);
        }


        private void OnEnable()
        {
            Character_Movement.OnLoseLevel.AddListener(() => _loseGamePanel.gameObject.SetActive(true));
        }

        #endregion
    }
}