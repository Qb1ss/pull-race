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

        #endregion

        [Header("Anchors")]
        [SerializeField] private Transform[] _leftAnchors;
        [SerializeField] private Transform[] _rightAnchors;
        [Space(height: 5f)]

        [SerializeField] private LineRenderer _line;
        private LineRenderer[] _lines;

        [Header("Parameters")]
        [SerializeField] private Transform _startBorder;
        [SerializeField] private Transform _character;
        [Space(height: 5f)]

        [SerializeField] private float _maxDistanceTencion = 4f;
        [Space(height: 5f)]

        [SerializeField] private Joystick _joysticForceTencion;

        private bool _isStartingGame = false;

        private Vector3 _startPosition;
        private Vector3 _newPosition;


        #region MONO

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


        private void OnDisable()
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

            _newPosition = new Vector3(_character.position.x, _character.position.y, _newPosition.z + _character.position.y + 3.5f);

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

            /*
            float s = 1f;

            if (_newPosition.z < _startPosition.z)
            {
                _newPosition = new Vector3(_startBorder.position.x, _startBorder.position.y, _startBorder.position.z - s);

                _character.transform.position = _newPosition;

                RenderLines();
            }
            else if (_newPosition.z >= _startPosition.z)
            {
                yield break;
            }
            */
        }
    }
}