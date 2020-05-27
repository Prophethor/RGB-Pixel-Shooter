using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Revolver", menuName = "Weapons/Revolver")]
public class Revolver : Weapon {

    public Rigidbody2D bulletPrefab;

    public float fireRate = 1f;
    public float loadTime = 1f;

    private WeaponState state;
    private Queue<RGBColor> loadQueue;
    private Tween currentLoad;

    public int dmgAmount = 1;
    public float bulletSpeed = 5;
    private List<RGBColor> bullets;

    private static Dictionary<string, UnityEngine.Events.UnityAction> UIHooks;

    private Animator animator;
    private GameObject player;

    public override void LevelStart () {
        
        state = WeaponState.READY;
        bullets = new List<RGBColor>();
        loadQueue = new Queue<RGBColor>();

        player = GameObject.FindGameObjectWithTag("Player");
        animator = player.GetComponent<Animator>();
        animator.SetFloat("loadSpeed", 0.4f / loadTime);
        
        if (UIHooks == null) {
            InitHooks();
        }
    }

    public override string GetName () { return "Revolver"; }

    private void InitHooks () {
        UIHooks = new Dictionary<string, UnityEngine.Events.UnityAction>();
        UIHooks.Add("LoadRed", () => Load(RGBColor.RED));
        UIHooks.Add("LoadGreen", () => Load(RGBColor.GREEN));
        UIHooks.Add("LoadBlue", () => Load(RGBColor.BLUE));
        UIHooks.Add("Shoot", () => Shoot(GetPlayerPos()));
    }

    public Vector3 GetPlayerPos () {
        return GameObject.FindGameObjectWithTag("Player").transform.position;
    }

    public override void HookUI (Transform parentPanel) {
        if (UIHooks == null) {
            InitHooks();
        }

        for (int i = 0; i < parentPanel.transform.childCount; i++) {
            if (UIHooks.ContainsKey(parentPanel.GetChild(i).tag)) {
                parentPanel.GetChild(i).GetComponent<Button>().onClick.AddListener(UIHooks[parentPanel.GetChild(i).tag]);
            }
        }
    }

    public void UnhookUI (GameObject parentPanel) {
        for (int i = 0; i < parentPanel.transform.childCount; i++) {
            if (UIHooks.ContainsKey(parentPanel.transform.GetChild(i).tag)) {
                parentPanel.transform.GetChild(i).GetComponent<Button>().onClick.RemoveListener(UIHooks[parentPanel.transform.GetChild(i).tag]);
            }
        }
    }

    public void Load (RGBColor color) {
        if (state != WeaponState.READY ) {
            if (bullets.Count + loadQueue.Count < 6) {
                loadQueue.Enqueue(color);
            }
            return;
        }

        if (bullets.Count < 6 && !player.GetComponent<TestPlayer>().isJumping) {
            animator.SetTrigger("loadTrigger");
            SetState(WeaponState.LOADING);
            currentLoad = Tweener.Invoke(loadTime, () => {
                if (state == WeaponState.LOADING) {
                    bullets.Add(color);
                    SetState(WeaponState.READY);
                }
            });
        }
    }

    public override void Shoot (Vector3 position) {
        if ((state == WeaponState.READY || state == WeaponState.LOADING) && bullets.Count > 0 && !player.GetComponent<TestPlayer>().isJumping) {
            animator.SetTrigger("shootTrigger");
            Rigidbody2D bulletObj = Instantiate(bulletPrefab, (Vector3) deltaPosition + position, Quaternion.identity);
            bulletObj.velocity = new Vector2(bulletSpeed, 0);
            bulletObj.GetComponent<Projectile>().SetDamage(bullets[0], dmgAmount);
            bullets.RemoveAt(0);

            // Stop any and all bullet loading
            if (currentLoad.active) {
                Tweener.RemoveTween(currentLoad);
            }
            loadQueue.Clear();

            
            SetState(WeaponState.COOLDOWN);
            Tweener.Invoke(1f / fireRate, () => {
                SetState(WeaponState.READY);
            });
        }
    }
  
    public void SetState (WeaponState newState) {
        state = newState;
        if (newState == WeaponState.READY && loadQueue.Count > 0) {
            Load(loadQueue.Dequeue());
        }
    }
}
