using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using Scenes;

namespace Interface.EndGame
{
    public class WinGamePanel : MonoBehaviour
    {
        [Header("Parameters")]
        [SerializeField] private float _timeAnimation = 1f;

        [Header("Compenents")]
        [SerializeField] private Button _nextLevelButton;
        [Space(height: 5f)]

        [SerializeField] private TextMeshProUGUI _headerText;
        [SerializeField] private TextMeshProUGUI _buttonNextLevelText;
        [Space(height: 5f)]

        private Image _winGamePanel;

        private SceneTrancition _sceneTrancition;


        #region MONO

        private void Start()
        {
            _sceneTrancition = new SceneTrancition();

            _nextLevelButton.onClick.AddListener(() => TrancitionToNextLevel());

            UpdateStartVisual();
        }

        #endregion

        #region Private Methods

        private void UpdateStartVisual()
        {
            _winGamePanel = GetComponent<Image>();

            _nextLevelButton.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
            _headerText.color = new Color(1f, 1f, 1f, 1f);
            _buttonNextLevelText.color = new Color(0f, 0f, 0f, 0f);
            _winGamePanel.color = new Color(0f, 0f, 0f, 0f);

            StartingAnimation();
        }


        private void StartingAnimation()
        {
            _nextLevelButton.GetComponent<Image>().DOColor(new Color(1f, 1f, 1f, 1f), _timeAnimation);
            _headerText.DOColor(new Color(1f, 1f, 1f, 1f), _timeAnimation);
            _buttonNextLevelText.DOColor(new Color(0f, 0f, 0f, 1f), _timeAnimation);
            _winGamePanel.DOColor(new Color(0f, 0f, 0f, 1f), _timeAnimation);
        }


        private void TrancitionToNextLevel()
        {
            _sceneTrancition.OnTrancitionToNextScene();
        }

        #endregion
    }
}
