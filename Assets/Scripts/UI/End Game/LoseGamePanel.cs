using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using Scenes;

namespace Interface.EndGame
{
    public class LoseGamePanel : MonoBehaviour
    {
        [Header("Parameters")]
        [SerializeField] private float _timeAnimation = 1f;

        [Header("Compenents")]
        [SerializeField] private Button _restartLevelButton;
        [Space(height: 5f)]

        [SerializeField] private TextMeshProUGUI _headerText;
        [SerializeField] private TextMeshProUGUI _buttonRestartLevelText;
        [Space(height: 5f)]

        private Image _loseGamePanel;

        private SceneTrancition _sceneTrancition;


        #region MONO

        private void Start()
        {
            _sceneTrancition = new SceneTrancition();

            _restartLevelButton.onClick.AddListener(() => TrancitionToNextLevel());

            UpdateStartVisual();
        }

        #endregion

        #region Private Methods

        private void UpdateStartVisual()
        {
            _loseGamePanel = GetComponent<Image>();

            _restartLevelButton.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
            _headerText.color = new Color(1f, 1f, 1f, 1f);
            _buttonRestartLevelText.color = new Color(0f, 0f, 0f, 0f);
            _loseGamePanel.color = new Color(0f, 0f, 0f, 0f);

            StartingAnimation();
        }


        private void StartingAnimation()
        {
            _restartLevelButton.GetComponent<Image>().DOColor(new Color(1f, 1f, 1f, 1f), _timeAnimation);
            _headerText.DOColor(new Color(1f, 1f, 1f, 1f), _timeAnimation);
            _buttonRestartLevelText.DOColor(new Color(0f, 0f, 0f, 1f), _timeAnimation);
            _loseGamePanel.DOColor(new Color(0f, 0f, 0f, 1f), _timeAnimation);
        }


        private void TrancitionToNextLevel()
        {
            _sceneTrancition.OnTrancitionToNextScene();
        }

        #endregion
    }
}