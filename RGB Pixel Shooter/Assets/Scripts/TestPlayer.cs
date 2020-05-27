using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour {

    // Besto testo
    public Weapon equippedWeapon;
    public Weapon otherWeapon;
    public RectTransform playerSpace;
    private Animator animator;

    private int lane = 1;
    [HideInInspector]
    public int savedLane = 1;

    private bool isJumping = false;
    private float laneOffset;

    private void Start () {
        equippedWeapon.LevelStart();
        Swipe.OnSwipe += Move;

        laneOffset = PlayField.GetLanePosition(lane) - transform.position.y;

        animator = this.gameObject.GetComponent<Animator>();
    }

    private void Update () {
        if (Input.GetKeyDown(KeyCode.S)) {
            if (lane > 0) {
                SwitchLane(lane - 1);
            }
        }
        if (Input.GetKeyDown(KeyCode.W)) {
            if (lane < 2) {
                SwitchLane(lane + 1);
            }
        }

        if (Input.GetKeyDown(KeyCode.J)) {
            ((Revolver) equippedWeapon).Load(RGBColor.RED);
        }
        else if (Input.GetKeyDown(KeyCode.K)) {
            ((Revolver) equippedWeapon).Load(RGBColor.GREEN);
        }
        else if (Input.GetKeyDown(KeyCode.L)) {
            ((Revolver) equippedWeapon).Load(RGBColor.BLUE);
        }
        else if (Input.GetKeyDown(KeyCode.Space)) {
            ((Revolver) equippedWeapon).Shoot(transform.position);
        }
    }

    public void Move (Swipe.SwipeData swipe) {
        if (posOnPanel(Camera.main.ScreenToWorldPoint(swipe.startPos), playerSpace) &&
            swipe.direction == Swipe.SwipeDirection.Down) {
            SwitchLane(lane - 1);
        }
        else if (posOnPanel(Camera.main.ScreenToWorldPoint(swipe.startPos), playerSpace) &&
            swipe.direction == Swipe.SwipeDirection.Up) {
            SwitchLane(lane + 1);
        }
    }

    private void SwitchLane (int newLane) {
        if (newLane < 0 || 2 < newLane || isJumping) {
            return;
        }

        animator.SetBool("canShoot", false);
        animator.SetBool("canLoad", false);
        animator.SetTrigger("jumpTrigger");
        isJumping = true;

        Tweener.Invoke(0.3f, () => {
            Tweener.AddTween(() => transform.position.y, (x) => transform.position = new Vector3(transform.position.x, x, transform.position.z),
                PlayField.GetLanePosition(newLane)-laneOffset, 0.25f, TweenMethods.SoftEase, () => {
                    lane = newLane;
                    isJumping = false;
                    animator.SetBool("canShoot", true);
                    animator.SetBool("canLoad", true);
                });
        });

        
    }

    bool posOnPanel (Vector2 touch, RectTransform panel) {
        if ((touch.x >= panel.position.x && touch.x <= panel.position.x + panel.rect.width) &&
            (touch.y >= panel.position.y && touch.y <= panel.position.y + panel.rect.height)) return true;
        return false;
    }

}
