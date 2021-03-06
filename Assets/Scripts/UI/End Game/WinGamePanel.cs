using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using TMPro;
using DG.Tweening;
using Scenes;

namespace Interface.EndGame
{
    public class WinGamePanel : MonoBehaviour
    {
        #region EVENTS

        public static UnityEvent<int> OnGetCoins = new UnityEvent<int>();

        #endregion

        [Header("Parameters")]
        [SerializeField] private float _timeAnimation = 1f;

        [Header("Compenents")]
        [SerializeField] private Button _nextLevelButton;
        [Space(height: 5f)]

        [SerializeField] private TextMeshProUGUI _levelText;
        [SerializeField] private TextMeshProUGUI _getCoinText;

        private int _coinsValue;

        [Header("Coin Animation")]
        [SerializeField] private RectTransform _coinRectTransform;
        [Space(height: 5f)]

        [SerializeField] private float _timePlayingAnimation = 2f;
        [Space(height: 5f)]

        [SerializeField] private Vector2 _startPosition;
        private Vector2 _targetPosition;

        private SceneTrancition _sceneTrancition;


        #region MONO

        private void Awake()
        {
            EndGamePanels.OnWinLevel.AddListener(UpdateCoins);
        }

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
            _targetPosition = _coinRectTransform.anchoredPosition;
            _coinRectTransform.anchoredPosition = _startPosition;
            _coinRectTransform.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);

            _levelText.text = $"Level {_sceneTrancition.GetCurrentScene()}";

            StartCoroutine(ProgressBarCoroutine());
        }

        private void TrancitionToNextLevel()
        {
            _sceneTrancition.OnTrancitionToNextScene();
        }

        private void UpdateCoins(int value)
        {
            _coinsValue = value;

            _getCoinText.text = $"{_coinsValue}!";
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
