using UnityEngine;
using UnityEditor;

namespace Location
{
    [CustomEditor(typeof (GenerateManager))]
    public class GenerateManagerEditor : Editor
    {
        private SerializedProperty _sceneType = null;
        private SerializedProperty _chunkNumber = null;
        private SerializedProperty _isCoins = null;
        private SerializedProperty _clearChunkPrefab = null;
        private SerializedProperty _finishChunkPrefab = null;

        private SerializedProperty _carChunkPrefab = null;
        private SerializedProperty _blockChunkPrefab = null;
        private SerializedProperty _allChunkPrefab = null;

        private SerializedProperty _coinFequency = null;
        private SerializedProperty _coinPrefab = null;

        #region Private Methods

        private void OnEnable()
        {
            _sceneType = serializedObject.FindProperty("Type");
            _chunkNumber = serializedObject.FindProperty("ChunkNumber");
            _isCoins = serializedObject.FindProperty("IsCoins");

            _clearChunkPrefab = serializedObject.FindProperty("ClearChunkPrefab");
            _finishChunkPrefab = serializedObject.FindProperty("FinishChunkPrefab");

            _carChunkPrefab = serializedObject.FindProperty("CarChunkPrefab");
            _blockChunkPrefab = serializedObject.FindProperty("BlockChunkPrefab");
            _allChunkPrefab = serializedObject.FindProperty("AllChunkPrefab");

            _coinFequency = serializedObject.FindProperty("CoinFequency");
            _coinPrefab = serializedObject.FindProperty("CoinPrefab");
        }

        #endregion

        public override void OnInspectorGUI()
        {
            GenerateManager _generateManager = (GenerateManager)target;

            serializedObject.Update();

            EditorGUILayout.PropertyField(_sceneType);
            EditorGUILayout.PropertyField(_isCoins);
            EditorGUILayout.Space(5f);

            EditorGUILayout.PropertyField(_chunkNumber);
            EditorGUILayout.Space(5f);

            EditorGUILayout.PropertyField(_clearChunkPrefab);
            EditorGUILayout.PropertyField(_finishChunkPrefab);

            #region Location

            if (_generateManager.Type == LocationType.AllCar) EditorGUILayout.PropertyField(_carChunkPrefab);
            else if (_generateManager.Type == LocationType.AllBlock) EditorGUILayout.PropertyField(_blockChunkPrefab);
            else if (_generateManager.Type == LocationType.Default) EditorGUILayout.PropertyField(_allChunkPrefab);

            #endregion

            #region Coins

            if(_isCoins.boolValue == true)
            {
                EditorGUILayout.PropertyField(_coinFequency);
                EditorGUILayout.PropertyField(_coinPrefab);
            }

            #endregion

            serializedObject.ApplyModifiedProperties();
        }
    }
}