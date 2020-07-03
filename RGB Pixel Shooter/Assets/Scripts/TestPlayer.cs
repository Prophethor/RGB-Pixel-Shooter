using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour {

    // Besto testo
    public Weapon equippedWeapon;
    public RectTransform playerSpace;
    public GameObject swipeDetector;
    private Swipe swipe;
    private Animator animator;

    private int lane = 1;

    public float laneSwitchTime = 1f;

    [HideInInspector]
    public bool isJumping = false;
    private float laneOffset;

    private void Start () {

        swipe = Instantiate(swipeDetector).GetComponent<Swipe>();
        swipe.minDistanceForSwipe = 30;
        swipe.OnSwipe += Move;

        laneOffset = PlayField.GetLanePosition(lane) - transform.position.y;
        transform.position = new Vector3(Camera.main.ScreenToWorldPoint(new Vector3(0,0)).x*0.85f-1f,transform.position.y);
        animator = GetComponent<Animator>();
        animator.SetFloat("jumpSpeed", 0.15f/laneSwitchTime);
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
            ((TestRevolver) equippedWeapon).Load(RGBColor.RED);
        }
        else if (Input.GetKeyDown(KeyCode.K)) {
            ((TestRevolver) equippedWeapon).Load(RGBColor.GREEN);
        }
        else if (Input.GetKeyDown(KeyCode.L)) {
            ((TestRevolver) equippedWeapon).Load(RGBColor.BLUE);
        }
        else if (Input.GetKeyDown(KeyCode.Space)) {
            ((TestRevolver) equippedWeapon).Shoot(transform.position);
        }

        // ColorBomb test
        /*if (Input.GetMouseButtonDown(0)) {
            Debug.Log("Bombing");
            Consumable colorBomb = new ColorBomb();
            ((ColorBomb) colorBomb).radius = 5f;
            colorBomb.Use(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }*/
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
        //TODO: get rid of casting
        if (newLane < 0 || 2 < newLane || ((TestRevolver) equippedWeapon).isReloading) {
            return;
        }

        animator.SetTrigger("jumpTrigger");
        isJumping = true;
        lane = newLane;
        //if player keeps switching lanes for longer than laneSwitchTime, isJumping is gonna become false and he could shoot midair
        Tweener.AddTween(() => transform.position.y, (x) => transform.position = new Vector3(transform.position.x, x, transform.position.z),
            PlayField.GetLanePosition(newLane)-laneOffset, laneSwitchTime, TweenMethods.HardLog, () => {
                isJumping = false;
            });
    }

    bool posOnPanel (Vector2 touch, RectTransform panel) {
        if ((touch.x >= panel.position.x && touch.x <= panel.position.x + panel.rect.width) &&
            (touch.y >= panel.position.y && touch.y <= panel.position.y + panel.rect.height)) return true;
        return false;
    }
}
