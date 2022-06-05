using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "Upgrades", menuName = "Configs/Upgrades")]
    public class UpgradesConfigs : ScriptableObject
    {
        [Header("Upgrades Parameters")]
        [SerializeField] private float _timePlayingAnimations;
        [SerializeField] private float _yPosition;
        [Space(height: 5f)]

        [SerializeField] private float _multiplicationFactorPrice;
        [Space(height: 5f)]

        #region Public Fields
        public string[] NameButton;

        public float TimePlayingAnimations => _timePlayingAnimations;
        public float YPosition => _yPosition;
        public float MultiplicationFactorPrice => _multiplicationFactorPrice;

        #endregion
    }
}