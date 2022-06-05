using UnityEngine;
using System.Collections.Generic;
using Facebook.Unity;

namespace Analytics
{
    public class Analytics_Facebook : MonoBehaviour
    {
        public static Analytics_Facebook Instance;

        #region MONO

        private void Awake()
        {
            if (!FB.IsInitialized)
            {
                FB.Init(InitCallback, OnHideUnity);
            }
            else
            {
                FB.ActivateApp();
            }
        }

        private void Start()
        {
            Instance = this;

            DontDestroyOnLoad(this);
        }

        #endregion

        #region Public Methods

        public static void OnStartGame(int level)
        {
            var tutParams = new Dictionary<string, object>();
            tutParams[AppEventParameterName.ContentID] = $"start_game_{level}";
            tutParams[AppEventParameterName.Description] = $"Player Started Game!";
            tutParams[AppEventParameterName.Success] = $"{level}";

            FB.LogAppEvent(
                AppEventName.CompletedTutorial,
                parameters: tutParams
            );
        }


        public static void OnTrancitionCurrentLevel(int level)
        {
            var tutParams = new Dictionary<string, object>();
            tutParams[AppEventParameterName.ContentID] = $"player_trancition_to_{level}";
            tutParams[AppEventParameterName.Description] = $"Player Trancition To Current Level!";
            tutParams[AppEventParameterName.Success] = $"{level}";

            FB.LogAppEvent(
                AppEventName.CompletedTutorial,
                parameters: tutParams
            );
        }


        public static void OnTrancitionNextLevel(int level)
        {
            var tutParams = new Dictionary<string, object>();
            tutParams[AppEventParameterName.ContentID] = $"player_trancition_to_{level}";
            tutParams[AppEventParameterName.Description] = $"Player Trancition To Next Level!";
            tutParams[AppEventParameterName.Success] = $"{level}";

            FB.LogAppEvent(
                AppEventName.CompletedTutorial,
                parameters: tutParams
            );
        }


        public static void OnTrancitionLastLevel(int level)
        {
            var tutParams = new Dictionary<string, object>();
            tutParams[AppEventParameterName.ContentID] = $"player_trancition_to_{level}";
            tutParams[AppEventParameterName.Description] = $"Player Trancition To Last Level ({level})!";
            tutParams[AppEventParameterName.Success] = $"{level}";

            FB.LogAppEvent(
                AppEventName.CompletedTutorial,
                parameters: tutParams
            );
        }

        #endregion

        #region Private Methods

        private void InitCallback()
        {
            if (FB.IsInitialized)
            {
                FB.ActivateApp();
            }
            else
            {
                Debug.Log("Failed to Initialize the Facebook SDK");
            }
        }

        private void OnHideUnity(bool isGameShown)
        {
            if (!isGameShown)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
        }

        #endregion
    }
}