using UnityEngine;
using Character;
using MoreMountains.NiceVibrations;

namespace GameCamera
{
    public class Camera_Following : MonoBehaviour
    {
        [Header("Parameters")]
        [SerializeField] private float _distanceFollow = 5f;
        
        [SerializeField] private Vector3 _offset;

        [SerializeField] private HapticTypes _hapticTypes;
        
        private Character_Movement _character;


        #region MONO

        private void Awake()
        {
            _character = FindObjectOfType<Character_Movement>();
        }


        private void OnEnable()
        {
            Character_Movement.OnCrash.AddListener(OnShake);
        }

        #endregion

        private void Update()
        {
            CharacterFollowing();
        }

        #region Private Methods

        private void CharacterFollowing()
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, _character.transform.position.z - _distanceFollow - _offset.z);
        }


        private void OnShake()
        {
            MMVibrationManager.Haptic(_hapticTypes, false, true, this);
        }

        #endregion
    }
}