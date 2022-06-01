using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "Character Level ", menuName = "Configs/Characters")]
    public class CharacterParametersConfig : ScriptableObject
    {
        [Header("Character Parameters")]
        [SerializeField] private float _movementSpeed;
        [Space(height: 5f)]

        [SerializeField] private float _movementTimer;


        #region Public Fields

        public float MovementSpeed => _movementSpeed;
        public float MovementTimer => _movementTimer;

        #endregion
    }
}