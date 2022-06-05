using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "Upgrades", menuName = "Configs/Upgrades")]
    public class UpgradesConfigs : ScriptableObject
    {
        [Header("Upgrades Parameters")]
        [SerializeField] private float _timePlayingAnimations;
        [SerializeField] private float _yPosition;

        #region Public Methods

        public float TimePlayingAnimations => _timePlayingAnimations;
        public float YPosition => _yPosition;

        #endregion
    }
}