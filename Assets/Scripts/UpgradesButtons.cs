using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;
using Configs;

namespace Character.Upgrades
{
    public class UpgradesButtons : MonoBehaviour
    {
        [SerializeField] private UpgradesConfigs _parameters;

        [Header("Panels")]
        [SerializeField] private RectTransform _upgradeSlingshotPanel;
        [SerializeField] private RectTransform _upgradeForceCarPanel;
        [SerializeField] private RectTransform _upgradeTimeMovingCarPanel;

        [Header("Start Game")]
        [SerializeField] private Button _startGameButton;

        #region Private Fields

        private float _timePlayingAnimations => _parameters.TimePlayingAnimations;
        private float _yPosition => _parameters.YPosition;

        #endregion


        #region MONO

        private void Start()
        {
            OneningPanels();

            _startGameButton.onClick.AddListener(() => ClosingPanels());
        }

        #endregion

        #region Private Methods

        private void OneningPanels()
        {
            _upgradeSlingshotPanel.gameObject.transform.position = new Vector2(_upgradeSlingshotPanel.gameObject.transform.position.x, -_upgradeSlingshotPanel.gameObject.transform.position.y);
            _upgradeForceCarPanel.gameObject.transform.position = new Vector2(_upgradeForceCarPanel.gameObject.transform.position.x, -_upgradeForceCarPanel.gameObject.transform.position.y);
            _upgradeTimeMovingCarPanel.gameObject.transform.position = new Vector2(_upgradeTimeMovingCarPanel.gameObject.transform.position.x, -_upgradeTimeMovingCarPanel.gameObject.transform.position.y);

            _upgradeSlingshotPanel.transform.DOMoveY(_yPosition, _timePlayingAnimations);
            _upgradeForceCarPanel.transform.DOMoveY(_yPosition, _timePlayingAnimations);
            _upgradeTimeMovingCarPanel.transform.DOMoveY(_yPosition, _timePlayingAnimations);
        }


        private void ClosingPanels()
        {
            StartCoroutine(StartGameCoroutine());
        }

        #endregion

        private IEnumerator StartGameCoroutine()
        {
            _upgradeSlingshotPanel.transform.DOMoveY(-_yPosition, _timePlayingAnimations);
            _upgradeForceCarPanel.transform.DOMoveY(-_yPosition, _timePlayingAnimations);
            _upgradeTimeMovingCarPanel.transform.DOMoveY(-_yPosition, _timePlayingAnimations);

            yield return new WaitForSeconds(_timePlayingAnimations);

            gameObject.SetActive(false);

            yield break;
        }
    }
}