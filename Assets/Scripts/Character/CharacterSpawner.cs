using UnityEngine;

namespace Character.Slingshot
{
    public class CharacterSpawner : MonoBehaviour
    {
        #region CONST

        private const string SKINS_PLAYER_PREFS = "SkinsPlayerPrefs";

        #endregion

        [SerializeField] private Character_Movement[] _characters;
        [SerializeField] private Slingshot _slingshot = null;


        private void Awake()
        {
            Instantiate(_characters[PlayerPrefs.GetInt(SKINS_PLAYER_PREFS, 0)], gameObject.transform.position, Quaternion.identity);

            _slingshot.enabled = true;
        }
    }
}