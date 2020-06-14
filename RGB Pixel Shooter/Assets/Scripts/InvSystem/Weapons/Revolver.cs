using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Revolver", menuName = "Weapons/Revolver")]
public class Revolver : Weapon {

    public Rigidbody2D bulletPrefab;

    public float fireRate = 1f;
    public float loadTime = 5f;

    private WeaponState state;
    private Queue<RGBColor> loadQueue;
    private Tween currentLoad;

    public int dmgAmount = 1;
    public float bulletSpeed = 5;
    private List<RGBColor> bullets;

    private Animator animator;
    private GameObject player;
    private ReloadTimer timer;

    public override void LevelStart () {
        state = WeaponState.READY;
        bullets = new List<RGBColor>();
        for (int i = 0; i < 5; i++)
        {
            bullets.Add(RGBColor.NONE);
        }
        loadQueue = new Queue<RGBColor>();

        player = GameObject.FindGameObjectWithTag("Player");
        animator = player.GetComponent<Animator>();
        animator.SetFloat("loadSpeed", 0.4f / loadTime);

        timer = FindObjectOfType<ReloadTimer>();

        if (UIHooks == null) {
            InitHooks();
        }
    }

    public override string GetName () { return "Revolver"; }

    protected override void InitHooks () {
        UIHooks = new Dictionary<string, UnityEngine.Events.UnityAction>();
        UIHooks.Add("LoadRed", () => Load(RGBColor.RED));
        UIHooks.Add("LoadGreen", () => Load(RGBColor.GREEN));
        UIHooks.Add("LoadBlue", () => Load(RGBColor.BLUE));
        UIHooks.Add("Shoot", () => Shoot(GetPlayerPos()));
    }

    public Vector3 GetPlayerPos () {
        return GameObject.FindGameObjectWithTag("Player").transform.position;
    }
    public void ReloadNeutral(RGBColor color = RGBColor.NONE)
    {
        SetState(WeaponState.LOADING);
        animator.SetTrigger("loadTrigger");
        timer.StartTimer(loadTime);
        currentLoad = Tweener.Invoke(loadTime, () =>
        {
            bullets.Add(color);
            bullets.Add(RGBColor.NONE);
            bullets.Add(RGBColor.NONE);
            bullets.Add(RGBColor.NONE);
            bullets.Add(RGBColor.NONE);
            bullets.Add(RGBColor.NONE);

            SetState(WeaponState.READY);

        });

    }
    public void Load (RGBColor color) { //settig load to be like infuse, keeping name out of laziness

        if (state != WeaponState.LOADING)
        {
            if (bullets.Count == 0)
            {
                ReloadNeutral(color);
            
            }
            else
            {
                for (int i = 0; i < bullets.Count; i++)
                {
                    if (bullets[i] == RGBColor.NONE)
                    {
                        bullets[i] = color;
                        break;
                    }
                }
                    
            }

        }

        /* //Not going to enque anything since no timer is present
        if (state != WeaponState.READY) {
            if (bullets.Count + loadQueue.Count < 6) {
                loadQueue.Enqueue(color);
            }
            return;
        }*/
        /*
        if (bullets.Count < 6 && !player.GetComponent<TestPlayer>().isJumping) {
            animator.SetTrigger("loadTrigger");
            SetState(WeaponState.LOADING);
            currentLoad = Tweener.Invoke(loadTime, () => {
                if (state == WeaponState.LOADING) {
                    bullets.Add(color);
                    SetState(WeaponState.READY);
                }
            });
        } */
    }

    public override void Shoot (Vector3 position) {
        if (state == WeaponState.READY && bullets.Count > 0 && !player.GetComponent<TestPlayer>().isJumping) { // removed option to shoot while reloading
            animator.SetTrigger("shootTrigger");
            Rigidbody2D bulletObj = Instantiate(bulletPrefab, (Vector3) deltaPosition + position, Quaternion.identity);
            bulletObj.velocity = new Vector2(bulletSpeed, 0);
            bulletObj.GetComponent<Projectile>().SetDamage(bullets[0], dmgAmount);
            bullets.RemoveAt(0);

            /* Stop any and all bullet loading
            if (currentLoad.active) {
                Tweener.RemoveTween(currentLoad);
            }
            loadQueue.Clear();
            */

            SetState(WeaponState.COOLDOWN);
            Tweener.Invoke(1f / fireRate, () => {
                SetState(WeaponState.READY);
            });
        }

        if (state == WeaponState.READY && bullets.Count == 0 && !player.GetComponent<TestPlayer>().isJumping)
        {
            ReloadNeutral();
        }
        /*
        else // hopefully this means it fires colorless bullets when empty
        {
            animator.SetTrigger("shootTrigger");
            Rigidbody2D bulletObj = Instantiate(bulletPrefab, (Vector3)deltaPosition + position, Quaternion.identity);
            bulletObj.velocity = new Vector2(bulletSpeed, 0);
            bulletObj.GetComponent<Projectile>().SetDamage(RGBColor.NONE, dmgAmount);
        } */
    }

    public void SetState (WeaponState newState) {
        state = newState;
        if (newState == WeaponState.READY && loadQueue.Count > 0) {
            Load(loadQueue.Dequeue());
        }
    }
}
