using UnityEngine;

namespace Character.Slingshot
{
    public class CharacterSpawner : MonoBehaviour
    {
        [SerializeField] private Character_Movement[] _characters;
        [SerializeField] private Slingshot _slingshot = null;


        private void Awake()
        {
            //проверка на скин

            Instantiate(_characters[Random.Range(0, _characters.Length)], gameObject.transform.position, Quaternion.identity);

            _slingshot.enabled = true;
        }
    }
}