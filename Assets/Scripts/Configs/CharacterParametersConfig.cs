using UnityEngine;
using Interface.Upgrades;

namespace Configs
{
    [CreateAssetMenu(fileName = "Character Level ", menuName = "Configs/Characters")]
    public class CharacterParametersConfig : ScriptableObject
    {
        [Header("Character Parameters")]
        [SerializeField] private float _movementSpeed;
        [Space(height: 5f)]

        [SerializeField] private float _constMovementTimer;
        [SerializeField] private float _slowerMovementTimer;
        [Space(height: 5f)]

        [SerializeField] [Min(1)] private float _forceTensionSlingshot = 1;

        #region Public Fields

        public float MovementSpeed => _movementSpeed;
        public float ConstMovementTimer => _constMovementTimer;
        public float SlowerMovementTimer => _slowerMovementTimer;
        public float ForceTensionSlingshot => _forceTensionSlingshot;

        #endregion


        private void OnEnable()
        {
            
        }


        private void OnDisable()
        {

        }
    }
}