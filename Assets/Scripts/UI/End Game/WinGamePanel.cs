using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using TMPro;
using DG.Tweening;
using Scenes;
using Character;

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

        [SerializeField] private TextMeshProUGUI _headerText;
        [SerializeField] private TextMeshProUGUI _buttonNextLevelText;

        private Image _winGamePanel;

        private int _coinDivision = 10;
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

        private void Start()
        {
            _sceneTrancition = new SceneTrancition();

            _nextLevelButton.onClick.AddListener(() => TrancitionToNextLevel());

            UpdateStartVisual();

            _targetPosition = _coinRectTransform.anchoredPosition;
            _coinRectTransform.anchoredPosition = _startPosition;
            _coinRectTransform.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
        }

        #endregion

        #region Private Methods

        private void UpdateStartVisual()
        {
            _winGamePanel = GetComponent<Image>();

            _nextLevelButton.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
            _headerText.color = new Color(1f, 1f, 1f, 0f);
            _buttonNextLevelText.color = new Color(1f, 1f, 1f, 0f);
            _winGamePanel.color = new Color(0f, 0f, 0f, 0f);

            StartingAnimation();
        }


        private void StartingAnimation()
        {
            _nextLevelButton.GetComponent<Image>().DOColor(new Color(1f, 1f, 1f, 1f), _timeAnimation);
            _headerText.DOColor(new Color(1f, 1f, 1f, 1f), _timeAnimation);
            _buttonNextLevelText.DOColor(new Color(1f, 1f, 1f, 1f), _timeAnimation);
            _winGamePanel.DOColor(new Color(0f, 0f, 0f, 1f), _timeAnimation);

            StartCoroutine(ProgressBarCoroutine());
        }


        private void TrancitionToNextLevel()
        {
            _sceneTrancition.OnTrancitionToNextScene();
        }

        #endregion

        private IEnumerator ProgressBarCoroutine()
        {
            OnGetCoins?.Invoke(_coinsValue);

            yield return new WaitForSeconds(_timeAnimation);

            _coinRectTransform.GetComponent<Image>().DOColor(new Color(1f, 1f, 1f, 1f), _timeAnimation / 2);
            _coinRectTransform.DOAnchorPos(_targetPosition, _timePlayingAnimation);

            yield return new WaitForSeconds(_timeAnimation / 2);

            _coinRectTransform.DOScale(new Vector3(0f, 0f, 0f), _timeAnimation / 2);

            yield break;
        }
    }
}
