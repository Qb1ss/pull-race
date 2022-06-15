using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Character;
using DG.Tweening;

namespace Interface
{
    public class ProgressBar : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] private TextMeshProUGUI _levelText;
        [Space(height:5f)]

        [SerializeField] private Image _fillImage;

        [SerializeField] private Color _startColor;
        [SerializeField] private Color _endColor;

        private Slider _timeSlider;


        #region MONO

        private void Awake()
        {
            _timeSlider = GetComponent<Slider>();
        }


        private void Start()
        {
            _timeSlider.value = _timeSlider.maxValue;

            PreStartUpdate();
        }


        private void OnEnable()
        {
            Character_Movement.OnStartedGame.AddListener(StartUpdate);
        }

        #endregion

        #region Private Methods

        private void PreStartUpdate()
        {
            _timeSlider.value = _timeSlider.maxValue;

            _fillImage.color = _startColor;

            int currentLevel = SceneManager.GetActiveScene().buildIndex + 1;
            _levelText.text = $"Level {currentLevel}";
        }


        private void StartUpdate(float value)
        {
            _timeSlider.maxValue = value;
            _timeSlider.value = _timeSlider.maxValue;

            UpdateSlider();
        }


        private void UpdateSlider()
        {
            _timeSlider.DOValue(0f, _timeSlider.maxValue);
            _fillImage.DOColor(_endColor, _timeSlider.maxValue);
        }

        #endregion
    }
}