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

        [Header("Components")]
        [SerializeField] private Button _restartLevelButton = null;
        [SerializeField] private Button _lastLevelButton = null;
        [SerializeField] private TextMeshProUGUI _getCoinText = null;
        [SerializeField] private TextMeshProUGUI _levelText;

        [Header("Coin Animation")]
        [SerializeField] private RectTransform _coinRectTransform = null;
        [Space(height: 5f)]

        [SerializeField] private float _timePlayingAnimation = 2f;
        [Space(height: 5f)]

        [SerializeField] private Vector2 _startPosition;
        private Vector2 _targetPosition;

        private SceneTrancition _sceneTrancition = null;

        #region MONO

        private void Awake()
        {
            EndGamePanels.OnLoseLevel.AddListener(UpdateCoins);
        }


        private void Start()
        {
            _sceneTrancition = new SceneTrancition();

            _restartLevelButton.onClick.AddListener(() => TrancitionToNextLevel());
            _lastLevelButton.onClick.AddListener(() => _sceneTrancition.OnTrancitionToLastScene());

            UpdateStartVisual();
        }

        #endregion

        #region Private Methods

        private void UpdateCoins(int value)
        {
            _getCoinText.text = $"You Get {value}!";
        }


        private void UpdateStartVisual()
        {
            _targetPosition = _coinRectTransform.anchoredPosition;
            _coinRectTransform.anchoredPosition = _startPosition;
            _coinRectTransform.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);

            _getCoinText.DOColor(new Color(1f, 1f, 1f, 1f), _timeAnimation);

            _levelText.text = $"Level {_sceneTrancition.GetCurrentScene()}";

            StartCoroutine(GetCoinCoroutine());
        }


        private void TrancitionToNextLevel()
        {
            _sceneTrancition.OnTrancitionToCurrentScene();
        }

        #endregion

        private IEnumerator GetCoinCoroutine()
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