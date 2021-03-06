using UnityEngine;
using System.Collections.Generic;
using Configs;

namespace Location
{
    public class LocationManager : MonoBehaviour
    {
        [SerializeField] private LevelConfig _parameters;

        private List<Chunk> _spawnedChunks = new List<Chunk>();

        #region Private Fields

        private int _chunkNumber => _parameters.ChunkPrefab.Length;

        private Chunk[] _chunkPrefab => _parameters.ChunkPrefab;
        private Chunk _finishChunkPrefab => _parameters.FinishChunkPrefab;

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
            for (int i = 0; i <= _chunkNumber; i++)
            {
                if (i < _chunkNumber)
                {
                    Chunk newChunk = Instantiate(_chunkPrefab[i]);

                    if (_spawnedChunks.Count != 0)
                    {
                        newChunk.transform.position = _spawnedChunks[_spawnedChunks.Count - 1].End.position - newChunk.Start.localPosition * 2;
                    }
                    else
                    {
                        newChunk.transform.position = gameObject.transform.position;
                    }

                    newChunk.Create();

                    _spawnedChunks.Add(newChunk);
                }
                else if (i == _chunkNumber)
                {
                    Chunk newChunk = Instantiate(_finishChunkPrefab);
                    newChunk.transform.position = _spawnedChunks[_spawnedChunks.Count - 1].End.position - newChunk.Start.localPosition * 2;
                    _spawnedChunks.Add(newChunk);
                }
            }
        }

        #endregion
    }
}