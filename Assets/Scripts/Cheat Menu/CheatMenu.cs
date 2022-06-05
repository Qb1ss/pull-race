using UnityEngine;
using UnityEngine.UI;
using Scenes;

namespace Cheat
{
    public class CheatMenu : MonoBehaviour
    {
        [Header("Buttons")]
        [SerializeField] private Button _currentLevelButton;
        [SerializeField] private Button _nextLevelButton;
        [SerializeField] private Button _lastLevelButton;

        private SceneTrancition _sceneTrancition;


        #region MONO

        private void Start()
        {
            _sceneTrancition = new SceneTrancition();

            _currentLevelButton.onClick.AddListener(() => OnTrancitionToCurrentScene());
            _nextLevelButton.onClick.AddListener(() => OnTrancitionToNextScene());
            _lastLevelButton.onClick.AddListener(() => OnTrancitionToLastScene());
        }

        #endregion

        #region Private Methods

        private void OnTrancitionToCurrentScene()
        {
            _sceneTrancition.OnTrancitionToCurrentScene();
        }

        private void OnTrancitionToNextScene()
        {
            _sceneTrancition.OnTrancitionToNextScene();
        }


        private void OnTrancitionToLastScene()
        {
            _sceneTrancition.OnTrancitionToLastScene();
        }

        #endregion
    }
}