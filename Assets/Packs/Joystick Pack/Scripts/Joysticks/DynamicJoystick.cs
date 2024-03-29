﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class DynamicJoystick : Joystick
{
    #region EVENTS

    public static UnityEvent<float> OnStartGame = new UnityEvent<float>();
    public static UnityEvent OnStarting = new UnityEvent();

    #endregion

    public float MoveThreshold { get { return moveThreshold; } set { moveThreshold = Mathf.Abs(value); } }

    [SerializeField] private float moveThreshold = 1;
    [Space(height: 5f)]

    [SerializeField] private bool _isCharacterController = true;

    protected override void Start()
    {
        MoveThreshold = moveThreshold;
        base.Start();
        background.gameObject.SetActive(false);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (_isCharacterController == false)
        {
            OnStarting?.Invoke();
        }

        background.anchoredPosition = ScreenPointToAnchoredPosition(eventData.position);
        background.gameObject.SetActive(true);
        base.OnPointerDown(eventData);

    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        background.gameObject.SetActive(false);

        if (_isCharacterController == false)
        {
            if (Vertical < 0)
            {
                OnStartGame?.Invoke(-Vertical);

                gameObject.SetActive(false);
            }
        }
        

        base.OnPointerUp(eventData);
    }

    protected override void HandleInput(float magnitude, Vector2 normalised, Vector2 radius, Camera cam)
    {
        if (magnitude > moveThreshold)
        {
            Vector2 difference = normalised * (magnitude - moveThreshold) * radius;
            background.anchoredPosition += difference;
        }
        base.HandleInput(magnitude, normalised, radius, cam);
    }
}