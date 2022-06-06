using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "Upgrades", menuName = "Configs/Upgrades")]
    public class UpgradesConfigs : ScriptableObject
    {
        [Header("Panels Parameters")]
        [SerializeField] private float _timePlayingAnimations;
        [SerializeField] private float _yPosition;

        [Header("Item Parameters")]
        [SerializeField] private float _multiplicationFactorPrice;
        [Space(height: 5f)]

        #region Public Fields
        public string[] NamesButton;

        public int[] StartPricesButton;

        public float TimePlayingAnimations => _timePlayingAnimations;
        public float YPosition => _yPosition;
        public float MultiplicationFactorPrice => _multiplicationFactorPrice;

        #endregion
    }
}