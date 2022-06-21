using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Character.Slingshot
{
    public class Slingshot : MonoBehaviour
    {
        #region CONSTS

        private const float DIVISION_SLINGSHOT_FORCE = 5f;
        private const float Z_POSITION = 4f;

        #endregion

        [Header("Anchors")]
        [SerializeField] private Transform[] _leftAnchors = null;
        [SerializeField] private Transform[] _rightAnchors = null;
        [Space(height: 5f)]

        [SerializeField] private LineRenderer _line = null;
        private LineRenderer[] _lines = null;

        [Header("Parameters")]
        [SerializeField] private Transform _startBorder = null;
        private Transform _character = null;
        [Space(height: 5f)]

        [SerializeField] private float _maxDistanceTencion = 4f;
        [Space(height: 5f)]

        [SerializeField] private Joystick _joysticForceTencion = null;

        private bool _isStartingGame = false;

        private Vector3 _startPosition;
        private Vector3 _newPosition;


        #region MONO

        private void Awake()
        {
            _character = FindObjectOfType<Character_Movement>().GetComponent<Transform>();
        }


        private void Start()
        {
            _startPosition = _startBorder.position;

            _lines = new LineRenderer[3];

            for (int i = 0; i < _lines.Length; i++)
            {
                _lines[i] = Instantiate(_line);
            }
        }


        private void OnEnable()
        {
            DynamicJoystick.OnStartGame.AddListener(OnStartGame);
        }

        #endregion

        private void Update()
        {
            TencioningSlingshot();
        }

        #region Private Methods

        private void TencioningSlingshot()
        {
            _newPosition = new Vector3(_startBorder.position.x, _startBorder.position.y, _startBorder.position.z + _joysticForceTencion.Vertical / DIVISION_SLINGSHOT_FORCE);

            if (_newPosition.z < _startPosition.z - _maxDistanceTencion)
            {
                return;
            }
            else if (_newPosition.z > _startPosition.z)
            {
                return;
            }

            _startBorder.position = _newPosition;

            if (_isStartingGame == true)
            {
                StartCoroutine(StartingCoroutine());
            }

            _newPosition = new Vector3(_character.position.x, _character.position.y, _newPosition.z + _character.position.y + Z_POSITION);

            _character.position = _newPosition;

            RenderLines();
        }


        private void OnStartGame(float force)
        {
            _isStartingGame = true;
        }


        private void RenderLines()
        {
            Vector3[] leftPosition = new Vector3[2];
            leftPosition[0] = _leftAnchors[0].position;
            leftPosition[1] = new Vector3(_leftAnchors[1].position.x + 1f, _leftAnchors[1].position.y, _leftAnchors[1].position.z);

            Vector3[] rightPosition = new Vector3[2];
            rightPosition[0] = _rightAnchors[0].position;
            rightPosition[1] = new Vector3(_rightAnchors[1].position.x - 1f, _rightAnchors[1].position.y, _rightAnchors[1].position.z);

            Vector3[] centerPosition = new Vector3[2];
            centerPosition[0] = new Vector3(_leftAnchors[1].position.x + 0.75f, _leftAnchors[1].position.y, _leftAnchors[1].position.z);
            centerPosition[1] = new Vector3(_rightAnchors[1].position.x - 0.75f, _rightAnchors[1].position.y, _rightAnchors[1].position.z);

            _lines[0].SetPositions(leftPosition);
            _lines[1].SetPositions(rightPosition);
            _lines[2].SetPositions(centerPosition);
        }

        #endregion

        private IEnumerator StartingCoroutine()
        {
            float time = 0.1f;

            yield return new WaitForSeconds(time);

            _startBorder.position = _newPosition;

            RenderLines();

            yield break;
        }
    }
}