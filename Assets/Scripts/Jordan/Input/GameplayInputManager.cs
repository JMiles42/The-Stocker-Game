using System;
using System.Collections.Generic;
using JMiles42.Extensions;
using JMiles42.Generics;
using JMiles42.Systems.InputManager;
using JMiles42.UnityInterfaces;
using UnityEngine;

public class GameplayInputManager: Singleton<GameplayInputManager>, IEventListening
{
    public InputAxis PrimaryClick = "Fire1";
    public InputAxis SecondaryClick = "Fire2";

    public List<SavedTouchData> TouchList = new List<SavedTouchData>(2);
    public float TimeForAlternateTouch = 0.2f;

    public event Action<Vector2> OnPrimaryClick = (a) => {};
    public event Action<Vector2> OnSecondaryClick = (a) => {};

    public void OnEnable()
    {
        Input.simulateMouseWithTouches = false;
        Input.backButtonLeavesApp = false;

        PrimaryClick.onKeyDown += OnPrimaryKeyDown;
        SecondaryClick.onKeyDown += OnSecondaryKeyDown;
    }

    void Update()
    {
        if (Input.touchCount == 0)
            return;

        for (var i = 0; i < Input.touchCount; i++)
        {
            var touch = Input.GetTouch(i);
            AddToTouchList(touch);
        }
        CalculateTouchList();
    }

    private void OnPrimaryKeyDown() { OnPrimaryClick.Trigger(Input.mousePosition); }

    private void OnSecondaryKeyDown() { OnSecondaryClick.Trigger(Input.mousePosition); }

    public void OnDisable()
    {
        PrimaryClick.onKeyDown -= OnPrimaryKeyDown;
        SecondaryClick.onKeyDown -= OnSecondaryKeyDown;
    }

    private void AddToTouchList(Touch touch)
    {
        switch (touch.phase)
        {
            case TouchPhase.Began:
                TouchList.Add(new SavedTouchData(touch));
                break;
            case TouchPhase.Moved:
                break;
            case TouchPhase.Stationary:
                break;
            case TouchPhase.Ended:
                break;
            case TouchPhase.Canceled:
                break;
        }
    }

    private void CalculateTouchList()
    {
        for (int i = TouchList.Count - 1; i >= 0; i--)
        {
            var resualts = TouchList[i].DoTouch();
            if (resualts.Over)
            {}
        }
    }

    public bool WasAltTouch(float time)
    {
        if (time >= TimeForAlternateTouch)
            return true;
        return false;
    }

    public class SavedTouchData
    {
        public Touch Touch;
        public float StartTime;
        public Vector2 StartPos;

        public SavedTouchData(Touch touch)
        {
            Touch = touch;
            StartPos = touch.position;
            StartTime = Time.time;
        }

        public TouchEndData DoTouch()
        {
            var touch = new TouchEndData {Over = Touch.phase == TouchPhase.Ended, Data = this, HeldTime = Time.time - StartTime};
            return touch;
        }

        public struct TouchEndData
        {
            public bool Over;
            public SavedTouchData Data;
            public float HeldTime;
        }
    }
}