using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour {

    // Besto testo
    public Weapon equippedWeapon;
    public RectTransform moveSpace;
    public RectTransform shootSpace;
    public GameObject swipeDetector;
    private Swipe swipe;
    private Animator animator;

    private int lane = 1;

    public float laneSwitchTime = 1f;

    [HideInInspector]
    public bool isJumping = false;
    private Tween jumpTween;
    private float laneOffset;


    public AudioClip allPurposeAudio;
    public AudioClip moveSFX;

    private void Start () {


        swipe = Instantiate(swipeDetector).GetComponent<Swipe>();
        swipe.minDistanceForSwipe = 20;
        swipe.detectOnlyAfterSwipe = true;
        swipe.OnSwipe += Move;

        transform.position = new Vector3(Camera.main.ScreenToWorldPoint(new Vector3(0, 0)).x +
                   0.05f * Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, 0)).x, transform.position.y);

        Transform finishLine = GameObject.FindGameObjectWithTag("FinishLine").transform;
        finishLine.position = new Vector3((transform.position + new Vector3(2, 0, 0)).x,finishLine.position.y);

        laneOffset = PlayField.GetLanePosition(lane) - transform.position.y;
        animator = GetComponent<Animator>();
        animator.runtimeAnimatorController = equippedWeapon.controller;
        animator.SetFloat("jumpSpeed", 0.2f / laneSwitchTime);
    }

    private void Update () {

        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began) {
            if (PosOnPanel(Camera.main.ScreenToWorldPoint(Input.touches[0].position), shootSpace)) {
                equippedWeapon.Shoot();
            }
        }

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

        if (Input.GetKeyDown(KeyCode.O)) {
            FindObjectOfType<UITest>().SwitchWeapon();
        }

        if (Input.GetKeyDown(KeyCode.J)) {
            equippedWeapon.Load(RGBColor.RED);
        }
        else if (Input.GetKeyDown(KeyCode.K)) {
            equippedWeapon.Load(RGBColor.GREEN);
        }
        else if (Input.GetKeyDown(KeyCode.L)) {
            equippedWeapon.Load(RGBColor.BLUE);
        }
        else if (Input.GetKeyDown(KeyCode.Space)) {
            equippedWeapon.Shoot();
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
        if (PosOnPanel(Camera.main.ScreenToWorldPoint(swipe.startPos), moveSpace) &&
            swipe.direction == Swipe.SwipeDirection.Down) {
            SwitchLane(lane - 1);
        }
        else if (PosOnPanel(Camera.main.ScreenToWorldPoint(swipe.startPos), moveSpace) &&
            swipe.direction == Swipe.SwipeDirection.Up) {
            SwitchLane(lane + 1);
        }
    }

    private void SwitchLane (int newLane) {
        //TODO: get rid of casting
        if (newLane < 0 || 2 < newLane || (equippedWeapon.CanMove())) {
            return;
        }
        allPurposeAudio = moveSFX;
        PlaySound();
        animator.SetTrigger("jumpTrigger");
        isJumping = true;
        lane = newLane;

        if (jumpTween != null && jumpTween.active == true) {
            jumpTween.active = false;
        }

        jumpTween = Tweener.AddTween(() => transform.position.y, (x) => transform.position = new Vector3(transform.position.x, x, transform.position.z),
            PlayField.GetLanePosition(newLane) - laneOffset, laneSwitchTime, TweenMethods.HardLog, () => {
                if (jumpTween.active == false) {
                    isJumping = false;
                }
            });
    }

    bool PosOnPanel (Vector2 touch, RectTransform panel) {
        if ((touch.x >= panel.position.x && touch.x <= panel.position.x + panel.rect.width) &&
            (touch.y >= panel.position.y && touch.y <= panel.position.y + panel.rect.height)) return true;
        return false;
    }

    public void PlaySound()
    {
        AudioManager.GetInstance().PlaySound(allPurposeAudio);
    }
}
