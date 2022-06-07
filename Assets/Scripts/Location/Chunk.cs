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

                Debug.Log($"Chunk: {NumberChunk}");

                Instantiate(_obstructions[Random.Range(0, _obstructions.Count)], gameObject.transform.position, Quaternion.identity);
            }
        }

        #endregion
    }
}