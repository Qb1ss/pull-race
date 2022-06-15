using UnityEngine;
using Character;

namespace GameCamera
{
    public class Camera_Following : MonoBehaviour
    {
        [Header("Parameters")]
        [SerializeField] private float _distanceFollow = 5f;
        
        [SerializeField] private Vector3 _offset;
        
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
            Handheld.Vibrate();
        }

        #endregion
    }
}