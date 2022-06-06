using UnityEngine;
using System.Collections.Generic;
using Configs;

namespace Location
{
    public class LocationManager : MonoBehaviour
    {
        [SerializeField] private LocationConfig _parameters;

        [SerializeField] private Chunk _groundChunkPrefab;

        private List<Chunk> _spawnedChunks = new List<Chunk>();

        #region Private Fields

        private int _chunkNumber => _parameters.ChunkNumber;

        #endregion


        #region MONO

        private void Start()
        {
            GenerateLocation();
        }

        #endregion

        #region Private Methods

        private void GenerateLocation()
        {
            for (int i = 0; i < _chunkNumber; i++)
            {
                Chunk newChunk = Instantiate(_groundChunkPrefab);

                if(_spawnedChunks.Count != 0)
                {
                    newChunk.transform.position = _spawnedChunks[_spawnedChunks.Count - 1].End.position - newChunk.Start.localPosition * 2;
                }
                else
                {
                    newChunk.transform.position = gameObject.transform.position;

                }

                _spawnedChunks.Add(newChunk);
            }
        }

        #endregion
    }
}