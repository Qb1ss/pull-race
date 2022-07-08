using UnityEngine;

namespace Character
{
    public class CharacterSpawner : MonoBehaviour
    {
        [SerializeField] private Character_Movement[] _characters;


        private void Awake()
        {
            //проверка на скин

            Instantiate(_characters[Random.Range(0, _characters.Length)], gameObject.transform.position, Quaternion.identity);
        }
    }
}