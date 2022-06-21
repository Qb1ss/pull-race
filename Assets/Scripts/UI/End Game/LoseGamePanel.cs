using UnityEngine;
using UnityEngine.UI;
using System.Collections;
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
        [SerializeField] private Button _restartLevelButton = null;
        [Space(height: 5f)]

        [SerializeField] private TextMeshProUGUI _headerText = null;
        [SerializeField] private TextMeshProUGUI _buttonRestartLevelText = null;
        [Space(height: 5f)]

        private Image _loseGamePanel = null;

        private SceneTrancition _sceneTrancition = null;

        [Header("Coin Animation")]
        [SerializeField] private RectTransform _coinRectTransform = null;
        [Space(height: 5f)]

        [SerializeField] private float _timePlayingAnimation = 2f;
        [Space(height: 5f)]

        [SerializeField] private Vector2 _startPosition;
        private Vector2 _targetPosition;


        #region MONO

        private void Awake()
        {
            EndGamePanels.OnLoseLevel.AddListener(UpdateCoins);
        }


        private void Start()
        {
            _sceneTrancition = new SceneTrancition();

            _restartLevelButton.onClick.AddListener(() => TrancitionToNextLevel());

            UpdateStartVisual();
        }

        #endregion

        #region Private Methods

        private void UpdateCoins(int value)
        {
            _headerText.text = $"You Get {value}!";

            StartingAnimation();
        }


        private void UpdateStartVisual()
        {
            _loseGamePanel = GetComponent<Image>();

            _restartLevelButton.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
            _headerText.color = new Color(1f, 1f, 1f, 0f);
            _buttonRestartLevelText.color = new Color(1f, 1f, 1f, 0f);
            _loseGamePanel.color = new Color(0f, 0f, 0f, 0f);

            _targetPosition = _coinRectTransform.anchoredPosition;
            _coinRectTransform.anchoredPosition = _startPosition;
            _coinRectTransform.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);

            StartingAnimation();
        }


        private void StartingAnimation()
        {
            _restartLevelButton.GetComponent<Image>().DOColor(new Color(1f, 1f, 1f, 1f), _timeAnimation);
            _headerText.DOColor(new Color(1f, 1f, 1f, 1f), _timeAnimation);
            _buttonRestartLevelText.DOColor(new Color(1f, 1f, 1f, 1f), _timeAnimation);
            _loseGamePanel.DOColor(new Color(0f, 0f, 0f, 1f), _timeAnimation);

            StartCoroutine(ProgressBarCoroutine());
        }


        private void TrancitionToNextLevel()
        {
            _sceneTrancition.OnTrancitionToCurrentScene();
        }

        #endregion

        private IEnumerator ProgressBarCoroutine()
        {
            yield return new WaitForSeconds(_timeAnimation);

            _coinRectTransform.GetComponent<Image>().DOColor(new Color(1f, 1f, 1f, 1f), _timeAnimation / 2);
            _coinRectTransform.DOAnchorPos(_targetPosition, _timePlayingAnimation);

            yield return new WaitForSeconds(_timeAnimation / 2);

            _coinRectTransform.DOScale(new Vector3(0f, 0f, 0f), _timeAnimation / 2);

            yield break;
        }
    }
}