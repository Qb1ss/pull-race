using UnityEngine;
using System.Collections.Generic;

namespace Location
{
    #region ENUMS

    public enum LocationType
    {
        AllCar = 0,
        AllBlock = 1,
        Default = 2
    }

    public enum BuildsType
    {
        Clear = 0,
        City = 1,
        Track = 2,
        Cave = 3
    }

    #endregion

    public class GenerateManager : MonoBehaviour
    {
        [Header("Paremeters")]
        public LocationType Type = LocationType.AllBlock;
        public int ChunkNumber = 0;

        public bool IsCoins = false;

        public int CoinFequency = 5;

        public CoinModel CoinPrefab = null;

        public Chunk[] CarChunkPrefab = null;
        public Chunk[] BlockChunkPrefab = null;
        public Chunk[] AllChunkPrefab = null;
        public Chunk ClearChunkPrefab = null;
        public Chunk FinishChunkPrefab = null;

        private List<Chunk> _spawnedChunks = new List<Chunk>();


        #region MONO

        private void Start()
        {
            if (Type == LocationType.AllBlock) GenerateAllBlockLocation();
            if (Type == LocationType.AllCar) GenerateAllCarLocation();
            if (Type == LocationType.Default) GenerateDefaultLocation();
        }

        #endregion

        #region Private Methods

        #region Location

        private void GenerateAllCarLocation()
        {
            int count = 0;

            for (int i = 0; i <= ChunkNumber; i++)
            {
                if (i < 2)
                {
                    Chunk newChunk = Instantiate(ClearChunkPrefab);

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
                else if (i >= 2 && i < ChunkNumber)
                {
                    Chunk newChunk = Instantiate(CarChunkPrefab[Random.Range(0, CarChunkPrefab.Length)]);

                    if (_spawnedChunks.Count != 0)
                    {
                        newChunk.transform.position = _spawnedChunks[_spawnedChunks.Count - 1].End.position - newChunk.Start.localPosition * 2;
                    }
                    else
                    {
                        newChunk.transform.position = gameObject.transform.position;
                    }

                    count++;

                    if (count == CoinFequency)
                    {
                        CheckCoinStatus(newChunk);

                        count = 0;
                    };

                    newChunk.Create();

                    _spawnedChunks.Add(newChunk);
                }
                else if (i == ChunkNumber)
                {
                    Chunk newChunk = Instantiate(FinishChunkPrefab);
                    newChunk.transform.position = _spawnedChunks[_spawnedChunks.Count - 1].End.position - newChunk.Start.localPosition * 2;
                    _spawnedChunks.Add(newChunk);
                }
            }
        }

        private void GenerateAllBlockLocation()
        {
            int count = 0;

            for (int i = 0; i <= ChunkNumber; i++)
            {
                if (i < 2)
                {
                    Chunk newChunk = Instantiate(ClearChunkPrefab);

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
                else if (i >= 2 && i < ChunkNumber)
                {
                    Chunk newChunk = Instantiate(BlockChunkPrefab[Random.Range(0, BlockChunkPrefab.Length)]);

                    if (_spawnedChunks.Count != 0)
                    {
                        newChunk.transform.position = _spawnedChunks[_spawnedChunks.Count - 1].End.position - newChunk.Start.localPosition * 2;
                    }
                    else
                    {
                        newChunk.transform.position = gameObject.transform.position;
                    }

                    count++;

                    if (count == CoinFequency)
                    {
                        CheckCoinStatus(newChunk);

                        count = 0;
                    }

                    newChunk.Create();

                    _spawnedChunks.Add(newChunk);
                }
                else if (i == ChunkNumber)
                {
                    Chunk newChunk = Instantiate(FinishChunkPrefab);
                    newChunk.transform.position = _spawnedChunks[_spawnedChunks.Count - 1].End.position - newChunk.Start.localPosition * 2;
                    _spawnedChunks.Add(newChunk);
                }
            }
        }

        private void GenerateDefaultLocation()
        {
            int count = 0;

            for (int i = 0; i <= ChunkNumber; i++)
            {
                 if (i < 2)
                {
                    Chunk newChunk = Instantiate(ClearChunkPrefab);

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
                else if (i >= 2 && i < ChunkNumber)
                {
                    Chunk newChunk = Instantiate(AllChunkPrefab[Random.Range(0, AllChunkPrefab.Length)]);

                    if (_spawnedChunks.Count != 0)
                    {
                        newChunk.transform.position = _spawnedChunks[_spawnedChunks.Count - 1].End.position - newChunk.Start.localPosition * 2;
                    }
                    else
                    {
                        newChunk.transform.position = gameObject.transform.position;
                    }

                    count++;

                    if (count == CoinFequency)
                    {
                        CheckCoinStatus(newChunk);

                        count = 0;
                    }

                    newChunk.Create();

                    _spawnedChunks.Add(newChunk);
                }
                else if (i == ChunkNumber)
                {
                    Chunk newChunk = Instantiate(FinishChunkPrefab);
                    newChunk.transform.position = _spawnedChunks[_spawnedChunks.Count - 1].End.position - newChunk.Start.localPosition * 2;
                    _spawnedChunks.Add(newChunk);
                }
            }
        }

        #endregion

        private void CheckCoinStatus(Chunk chunk)
        {
            if(IsCoins == true)
            {
                float xPosition = 7.75f;

                CoinModel coin = Instantiate
                    (
                    CoinPrefab, 
                    new Vector3(Random.Range(-xPosition, xPosition), 2f, chunk.gameObject.transform.position.z),
                    Quaternion.Euler(0f, 90f, 90f)
                    );
            }
        }

        #endregion
    }
}