using UnityEngine.SceneManagement;
using Analytics;

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

            Analytics_Facebook.OnTrancitionCurrentLevel(Level);
            Analytics_GameAnalytics.TransitionOnCurrentLevel(Level);

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

            Analytics_Facebook.OnTrancitionNextLevel(Level);
            Analytics_GameAnalytics.TransitionOnNextLevel(Level);

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

            Analytics_Facebook.OnTrancitionLastLevel(Level);
            Analytics_GameAnalytics.TransitionOnLastLevel(Level);

            SceneManager.LoadScene(Level);
        }

        #endregion

        #region Private Methods

        private void OnStartGame()
        {
            Analytics_GameAnalytics.OnStartGame(Level);
            Analytics_Facebook.OnStartGame(Level);
        }

        #endregion
    }
}