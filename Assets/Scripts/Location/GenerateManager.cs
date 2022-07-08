using UnityEngine;
using System.Collections.Generic;

namespace Location
{
    public class GenerateManager : MonoBehaviour
    {
        [Header("Paremeters")]
        [SerializeField] private int _chunkNumber = 0;
        [Space(height: 5f)]

        [SerializeField] private Chunk[] _chunkPrefab = null;
        [SerializeField] private Chunk _clearChunkPrefab = null;
        [SerializeField] private Chunk _finishChunkPrefab = null;

        private List<Chunk> _spawnedChunks = new List<Chunk>();


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
                if (i < 2)
                {
                    Chunk newChunk = Instantiate(_clearChunkPrefab);

                    if (_spawnedChunks.Count != 0)
                    {
                        newChunk.transform.position = _spawnedChunks[_spawnedChunks.Count - 1].End.position - newChunk.Start.localPosition * 2;
                    }
                    else
                    {
                        newChunk.transform.position = gameObject.transform.position;
                    }

                    _spawnedChunks.Add(newChunk);
                }
                else if (i >= 2 && i < _chunkNumber)
                {
                    Chunk newChunk = Instantiate(_chunkPrefab[Random.Range(0, _chunkPrefab.Length)]);

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