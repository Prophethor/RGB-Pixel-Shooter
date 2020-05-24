using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITest : MonoBehaviour {

    public Transform weapon1Panel;
    public Transform weapon2Panel;

    public TestPlayer player;

    private Vector2 fingerDown;
    private float fingerDownTime;
    private Vector2 fingerUp;
    private SwipeDirection direction;
    private float minDistanceForSwipe = 300f;
    public RectTransform weaponPanel;
    private bool weapon1Selected = true;

    public enum SwipeDirection {
        Up,
        Down,
        Left,
        Right
    }

    private void Start () {
        ((Revolver) player.equippedWeapon).HookUI(weapon1Panel);
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

    private void DetectSwipe () {
        bool onPan = posOnPanel(fingerDown, weaponPanel);
        if (SwipeDistanceCheckMet() && onPan) {
            if (VerticalMovementDistance() > HorizontalMovementDistance()) {
                direction = fingerDown.y - fingerUp.y > 0 ? SwipeDirection.Down : SwipeDirection.Up;
            }
            else {
                direction = fingerDown.x - fingerUp.x > 0 ? SwipeDirection.Left : SwipeDirection.Right;
            }
            if (direction == SwipeDirection.Down || direction == SwipeDirection.Up) {
                if (weapon1Selected) {
                    weapon1Panel.gameObject.SetActive(false);
                    weapon2Panel.gameObject.SetActive(true);
                    weapon1Selected = false;
                }
                else {
                    weapon1Panel.gameObject.SetActive(true);
                    weapon2Panel.gameObject.SetActive(false);
                    weapon1Selected = true;
                }
                Weapon tempWeap = player.equippedWeapon;
                player.equippedWeapon = player.otherWeapon;
                player.otherWeapon = tempWeap;
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

    bool posOnPanel (Vector2 touch, RectTransform panel) {
        if ((touch.x <= panel.position.x && touch.x >= panel.position.x - panel.rect.width) /*&& 
            (touch.y >= panel.position.y && touch.y <= panel.position.y + panel.rect.height)*/) return true;
        return false;
    }

    public void ButtonLog (string msg) {
        Debug.Log(msg);
    }
}
