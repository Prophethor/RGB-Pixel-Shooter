using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Swipe : MonoBehaviour
{
    private Vector2 fingerDown;
    private float fingerDownTime;
    private Vector2 fingerUp;
    private SwipeDirection direction;
    private float minDistanceForSwipe = 100f;

    public static event Action<SwipeData> OnSwipe = delegate { };

    public enum SwipeDirection {
        Up,
        Down,
        Left,
        Right
    }

    public struct SwipeData {
        public Vector2 startPos;
        public Vector2 endPos;
        public SwipeDirection direction;
    }

    private void DetectSwipe () {
        if (SwipeDistanceCheckMet()) {
            if (VerticalMovementDistance() > HorizontalMovementDistance()) {
                direction = fingerDown.y - fingerUp.y > 0 ? SwipeDirection.Down : SwipeDirection.Up;
                SendSwipe(direction);
            }
            else {
                direction = fingerDown.x - fingerUp.x > 0 ? SwipeDirection.Left : SwipeDirection.Right;
                SendSwipe(direction);
            }
        }
    }

    private bool SwipeDistanceCheckMet () {
        return VerticalMovementDistance() > minDistanceForSwipe || HorizontalMovementDistance() > minDistanceForSwipe;
    }

    private float VerticalMovementDistance () {
        return Mathf.Abs(fingerDown.y - fingerUp.y);
    }

    private float HorizontalMovementDistance () {
        return Mathf.Abs(fingerDown.x - fingerUp.x);
    }

    private void SendSwipe (SwipeDirection dir) {
        SwipeData swipeData = new SwipeData() {
            startPos = fingerDown,
            endPos = fingerUp,
            direction = dir

        };
        OnSwipe.Invoke(swipeData);
    }

    private void Update () {
        foreach (Touch touch in Input.touches) {
            if (touch.phase == TouchPhase.Began) {
                fingerDownTime = Time.time;
                fingerDown = touch.position;
            }
            if (touch.phase == TouchPhase.Ended) {
                fingerUp = touch.position;
                float timePassed = Time.time - fingerDownTime;
                if (timePassed < .4f) DetectSwipe();
            }
        }
    }

}
