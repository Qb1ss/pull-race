using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "Obstructions Level", menuName = "Configs/Obstructions")]
    public class ObstructionConfig : ScriptableObject
    {
        [Header("Car Parameters")]
        [SerializeField] private float _movementSpeed = 10f;

        #region Public Fields

        public float MovementSpeed => _movementSpeed;

        #endregion
    }
}