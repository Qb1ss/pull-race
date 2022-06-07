using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "Obstructions Level", menuName = "Configs/Obstructions")]
    public class ObstructionConfig : ScriptableObject
    {
        [Header("Main Parameters")]
        [SerializeField] private ParticleSystem _destroyEffect;

        [Header("Car Parameters")]
        [SerializeField] private float _movementSpeed = 10f;

        #region Public Fields

        public ParticleSystem DestroyEffect => _destroyEffect;

        public float MovementSpeed => _movementSpeed;

        #endregion
    }
}