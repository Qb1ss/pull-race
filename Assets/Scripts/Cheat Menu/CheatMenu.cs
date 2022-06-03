using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Cheat
{
    public class CheatMenu : MonoBehaviour
    {
        [Header("Buttons")]
        [SerializeField] private Button _currentLevelButton;
        [SerializeField] private Button _nextLevelButton;
        [SerializeField] private Button _lastLevelButton;


        #region MONO

        private void Start()
        {
            _currentLevelButton.onClick.AddListener(() => OnTrancitionToCurrentScene());
            _nextLevelButton.onClick.AddListener(() => OnTrancitionToNextScene());
            _lastLevelButton.onClick.AddListener(() => OnTrancitionToLastScene());
        }

        #endregion

        #region Private Methods

        private void OnTrancitionToCurrentScene()
        {
            int level = SceneManager.GetActiveScene().buildIndex;

            SceneManager.LoadScene(level);
        }

        private void OnTrancitionToNextScene()
        {
            int level = SceneManager.GetActiveScene().buildIndex;

            if(level >= SceneManager.sceneCountInBuildSettings - 1)
            {
                level = 0;
            }
            else
            {
                level++;
            }

            SceneManager.LoadScene(level);
        }


        private void OnTrancitionToLastScene()
        {
            int level = SceneManager.GetActiveScene().buildIndex;

            if (level <= 0)
            {
                level = SceneManager.sceneCountInBuildSettings - 1;
            }
            else
            {
                level--;
            }

            SceneManager.LoadScene(level);
        }

        #endregion
    }
}