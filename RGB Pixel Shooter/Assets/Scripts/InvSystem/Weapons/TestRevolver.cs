using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TestRevolver", menuName = "Weapons/TestRevolver")]
public class TestRevolver : Weapon
{

    public Rigidbody2D bulletPrefab;
    public int dmgAmount = 1;
    public float reloadTime = 3;

    [SerializeField]
    private List<RGBColor> bullets;
    private int maxBullets = 6;
    private float bulletSpeed = 15;
    public bool isReloading = false;

    private Animator animator;
    private GameObject player;

    public override string GetName () { return "Revolver"; }

    public override void LevelStart () {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = player.GetComponent<Animator>();
        animator.SetFloat("loadSpeed", 0.4f/reloadTime);
        bullets = new List<RGBColor>();
        while (bullets.Count < maxBullets) bullets.Add(RGBColor.NONE);
        if (UIHooks == null) {
            InitHooks();
        }
    }

    protected override void InitHooks () {
        UIHooks = new Dictionary<string, UnityEngine.Events.UnityAction>();
        UIHooks.Add("LoadRed", () => Load(RGBColor.RED));
        UIHooks.Add("LoadGreen", () => Load(RGBColor.GREEN));
        UIHooks.Add("LoadBlue", () => Load(RGBColor.BLUE));
        UIHooks.Add("Shoot", () => Shoot(GetPlayerPos()));
    }

    public void Load (RGBColor color) {
        for (int i = 0; i < bullets.Count; i++) {
            if (bullets[i] != RGBColor.NONE) continue;
            bullets[i] = color;
            break;
        }
    }

    public override void Shoot (Vector3 position) {
        if (!isReloading && !player.GetComponent<TestPlayer>().isJumping) {
            if (bullets.Count > 0) {
                animator.SetTrigger("shootTrigger");
                Rigidbody2D bulletObj = Instantiate(bulletPrefab, (Vector3) deltaPosition + position, Quaternion.identity);
                bulletObj.velocity = new Vector2(bulletSpeed, 0);
                bulletObj.GetComponent<Projectile>().SetDamage(bullets[0], dmgAmount);
                bullets.RemoveAt(0);
            }
            if(bullets.Count==0) {
                isReloading = true;
                Tweener.Invoke(0.15f,()=> {
                    animator.SetTrigger("loadTrigger");
                    Tweener.Invoke(reloadTime, () => {
                        while (bullets.Count < maxBullets) bullets.Add(RGBColor.NONE);
                        isReloading = false;
                    });
                });
            }
        }
    }

    public Vector3 GetPlayerPos () {
        return GameObject.FindGameObjectWithTag("Player").transform.position;
    }

}
