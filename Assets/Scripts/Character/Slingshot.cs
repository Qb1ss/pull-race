using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Character.Slingshot
{
    public class Slingshot : MonoBehaviour
    {
        [Header("Anchors")]
        [SerializeField] private Transform[] _leftAnchors;
        [SerializeField] private Transform[] _rightAnchors;
        [Space(height: 5f)]

        [SerializeField] private LineRenderer[] _lines;

        [Header("Parameters")]
        [SerializeField] private Transform _startBorder;
        [SerializeField] private Transform _character;
        [Space(height: 5f)]

        [SerializeField] private float _maxDistanceTencion = 4f;
        [Space(height: 5f)]

        [SerializeField] private Joystick _joysticForceTencion;

        private bool _isStartingGame = false;

        private Vector3 _startPosition;


        #region MONO

        private void Start()
        {
            _startPosition = _startBorder.position;
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
            Vector3 newPosition = new Vector3(_startBorder.position.x, _startBorder.position.y, _startBorder.position.z + _joysticForceTencion.Vertical / 20);

            if (newPosition.z < _startPosition.z - _maxDistanceTencion)
            {
                return;
            }
            else if (newPosition.z > _startPosition.z)
            {
                return;
            }

            _startBorder.position = newPosition;

            if (_isStartingGame == true)
            {
                RenderLines();

                _startBorder.position = _startPosition;

                return;
            }

            newPosition = new Vector3(_character.position.x, _character.position.y, _character.position.y / 2 + newPosition.z);

            _character.position = newPosition;

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
            leftPosition[1] = _leftAnchors[1].position;

            Vector3[] rightPosition = new Vector3[2];
            rightPosition[0] = _rightAnchors[0].position;
            rightPosition[1] = _rightAnchors[1].position;

            _lines[0].SetPositions(leftPosition);
            _lines[1].SetPositions(rightPosition);
        }

        #endregion
    }
}