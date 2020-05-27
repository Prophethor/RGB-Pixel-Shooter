using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour {

    // Besto testo
    public Weapon equippedWeapon;
    public Weapon otherWeapon;
    public RectTransform playerSpace;

    private int lane = 1;

    private void Start () {
        equippedWeapon.LevelStart();
        Swipe.OnSwipe += Move;
    }

    private void Update () {
        if (Input.GetKeyDown(KeyCode.S)) {
            if (lane > 0) {
                lane--;
            }
        }
        if (Input.GetKeyDown(KeyCode.W)) {
            if (lane < 2) {
                lane++;
            }
        }
        UpdatePosition();


        if (Input.GetKeyDown(KeyCode.J)) {
            ((Shotgun) equippedWeapon).Load(RGBColor.RED);
        }
        else if (Input.GetKeyDown(KeyCode.K)) {
            ((Shotgun) equippedWeapon).Load(RGBColor.GREEN);
        }
        else if (Input.GetKeyDown(KeyCode.L)) {
            ((Shotgun) equippedWeapon).Load(RGBColor.BLUE);
        }
        else if (Input.GetKeyDown(KeyCode.Space)) {
            equippedWeapon.Shoot(transform.position);
        }
    }

    public void Move (Swipe.SwipeData swipe) {
        if (posOnPanel(Camera.main.ScreenToWorldPoint(swipe.startPos), playerSpace) &&
            swipe.direction == Swipe.SwipeDirection.Down && lane > 0) {
            lane--;
        }
        else if (posOnPanel(Camera.main.ScreenToWorldPoint(swipe.startPos), playerSpace) &&
            swipe.direction == Swipe.SwipeDirection.Up && lane < 2) {
            lane++;
        }
        UpdatePosition();
    }

    bool posOnPanel (Vector2 touch, RectTransform panel) {
        if ((touch.x >= panel.position.x && touch.x <= panel.position.x + panel.rect.width) &&
            (touch.y >= panel.position.y && touch.y <= panel.position.y + panel.rect.height)) return true;
        return false;
    }

    private void UpdatePosition () {
        float yPos = PlayField.GetLanePosition(lane).y;
        transform.position = new Vector3(transform.position.x, yPos, yPos);
    }
}
