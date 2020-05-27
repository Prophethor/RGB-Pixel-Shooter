using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour {

    // Besto testo
    public Weapon equippedWeapon;
    public RectTransform playerSpace;
    private Animator animator;

    private int lane = 1;
    [HideInInspector]
    public int savedLane = 1;

    public bool isJumping = false;
    private float laneOffset;

    //TODO: Make it weapon dependant
    private float laneSwitchTime = 0.25f;

    private void Start () {
        equippedWeapon.LevelStart();
        Swipe.OnSwipe += Move;

        laneOffset = PlayField.GetLanePosition(lane) - transform.position.y;

        animator = GetComponent<Animator>();
        // * 2 bi bilo da želimo da se animacija završi u trenutku kad stigne na lejn, pa sam stavio * 1.5 da bismo imali mali delay
        animator.SetFloat("jumpSpeed", laneSwitchTime*1.5f);
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

        animator.SetTrigger("jumpTrigger");
        isJumping = true;

        Tweener.Invoke(0.3f, () => {
            Tweener.AddTween(() => transform.position.y, (x) => transform.position = new Vector3(transform.position.x, x, transform.position.z),
                PlayField.GetLanePosition(newLane)-laneOffset, laneSwitchTime, TweenMethods.SoftEase, () => {
                    lane = newLane;
                    isJumping = false;
                });
        });

        
    }

    bool posOnPanel (Vector2 touch, RectTransform panel) {
        if ((touch.x >= panel.position.x && touch.x <= panel.position.x + panel.rect.width) &&
            (touch.y >= panel.position.y && touch.y <= panel.position.y + panel.rect.height)) return true;
        return false;
    }

}
