using UnityEngine;

namespace Character
{
    public class Character_Wheel : MonoBehaviour
    {
        [SerializeField] private Character_Movement _character;


        private void Update()
        {
            gameObject.transform.Rotate(_character.MovingSpeed, 0f, 0f);
        }
    }
}