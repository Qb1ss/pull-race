using UnityEngine;
using GameAnalyticsSDK;

namespace Analytics
{
    public class Analytics_GameAnalytics : MonoBehaviour
    {
        public static Analytics_GameAnalytics Instance;


        #region MONO

        private void Start()
        {
            Instance = this;

            DontDestroyOnLoad(this);

            GameAnalytics.Initialize();
        }

        #endregion

        #region Public Methods

        public static void OnStartGame(int level)
        {
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, $"Start Game From {level}");
        }


        public static void TransitionOnCurrentLevel(int level)
        {
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, $"Transition On Current Level { level}");
        }


        public static void TransitionOnNextLevel(int level)
        {
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, $"Transition On Next Level { level}");
        }

        public static void TransitionOnLastLevel(int level)
        {
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, $"Transition On Last Level { level}");
        }

        #endregion
    }
}