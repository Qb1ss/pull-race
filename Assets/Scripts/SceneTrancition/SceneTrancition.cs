using UnityEngine.SceneManagement;

namespace Scenes
{
    public class SceneTrancition
    {
        public int Level;


        #region MONO

        private void Start()
        {
            Level = SceneManager.GetActiveScene().buildIndex;

            OnStartGame();
        }

        #endregion

        #region Public Methods

        public void OnTrancitionToCurrentScene()
        {
            Level = SceneManager.GetActiveScene().buildIndex;

            SceneManager.LoadScene(Level);
        }

        public void OnTrancitionToNextScene()
        {
            Level = SceneManager.GetActiveScene().buildIndex;

            if (Level >= SceneManager.sceneCountInBuildSettings - 1)
            {
                Level = 0;
            }
            else
            {
                Level++;

            }

            SceneManager.LoadScene(Level);
        }

        public void OnTrancitionToLastScene()
        {
            Level = SceneManager.GetActiveScene().buildIndex;

            if (Level <= 0)
            {
                Level = SceneManager.sceneCountInBuildSettings - 1;
            }
            else
            {
                Level--;
            }

            SceneManager.LoadScene(Level);
        }

        public int GetCurrentScene()
        {
            int currentLevel = SceneManager.GetActiveScene().buildIndex + 1;

            return currentLevel;
        }

        #endregion

        #region Private Methods

        private void OnStartGame()
        {
            
        }

        #endregion
    }
}