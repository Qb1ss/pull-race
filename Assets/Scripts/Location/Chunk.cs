using UnityEngine;
using System.Collections.Generic;
using Obstructions;

namespace Location
{
    public class Chunk : MonoBehaviour
    {
        [Header("Parameters")]
        public Transform Start;
        public Transform End;

        public int NumberChunk;

        private float _xSpawnerPosition = 8f;

        [SerializeField] private List<Obstruction> _obstructions = new List<Obstruction>();


        #region MONO

        public void Create()
        {
            if(NumberChunk >= 8)
            {
                if(_obstructions.Count == 0)
                {
                    return;
                }

                Obstruction obstruction = Instantiate(_obstructions[Random.Range(0, _obstructions.Count)]);
                obstruction.gameObject.transform.position = new Vector3(Random.Range(-_xSpawnerPosition, _xSpawnerPosition), gameObject.transform.position.y + obstruction.gameObject.transform.position.y / 2, gameObject.transform.position.z);
            }
        }

        #endregion
    }
}