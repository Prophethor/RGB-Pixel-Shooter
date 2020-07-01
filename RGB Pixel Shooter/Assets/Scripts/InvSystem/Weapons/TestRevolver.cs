using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TestRevolver", menuName = "Weapons/TestRevolver")]
public class TestRevolver : Weapon
{

    public Rigidbody2D bulletPrefab;
    public int dmgAmount = 100;
    public float reloadTime = 3;

    private List<RGBColor> bullets;
    private int maxBullets = 6;
    private float bulletSpeed = 15;
    [HideInInspector]
    public bool isReloading = false;

    private Animator animator;
    private GameObject player;

    private ReloadTimer timer;

    public override string GetName () { return "Revolver"; }

    public override void LevelStart () {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = player.GetComponent<Animator>();
        //animator.SetFloat("loadSpeed", 0.4f/reloadTime);
        animator.SetFloat("loadSpeed", (reloadTime - 0.3f)/(maxBullets*0.15f));
        bullets = new List<RGBColor>();
        while (bullets.Count < maxBullets) bullets.Add(RGBColor.NONE);
        timer = FindObjectOfType<ReloadTimer>();
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
    // TODO: this is infuse, not load... causes confusion
    public void Load (RGBColor color) {
        for (int i = 0; i < bullets.Count; i++) {
            if (bullets[i] != RGBColor.NONE) continue;
            AudioManager.instance.PlaySound(player.GetComponent<TestPlayer>().infuseSound, true);
            bullets[i] = color;
            break;
        }
    }

    public override void Shoot (Vector3 position) {
        if (!isReloading && !player.GetComponent<TestPlayer>().isJumping) {
            if (bullets.Count > 0) {
                animator.SetTrigger("shootTrigger");
                AudioManager.instance.PlaySound(player.GetComponent<TestPlayer>().shootSound, true);
                Rigidbody2D bulletObj = Instantiate(bulletPrefab, (Vector3) deltaPosition + position, Quaternion.identity);
                bulletObj.velocity = new Vector2(bulletSpeed, 0);
                bulletObj.GetComponent<Projectile>().SetDamage(bullets[0], dmgAmount);
                bullets.RemoveAt(0);
            }
            if(bullets.Count==0) {
                isReloading = true;
                timer.StartTimer(reloadTime); // this is for displaying reload timer somwhere
                animator.SetTrigger("loadTrigger");
                Tweener.Invoke(0f,()=> {
                    if (isReloading)
                    {
                        Tweener.Invoke(0f, () => {
                            animator.SetBool("canLoad", true);
                            LoadBullet(RGBColor.NONE);

                        });
                    }
                    
                   
                   

                    //Tweener.Invoke(reloadTime, () => {
                    //    while (bullets.Count < maxBullets) bullets.Add(RGBColor.NONE);
                    //    isReloading = false;
                    //});
                });
            }
        }
    }

    public void LoadBullet(RGBColor color)
    {
        
        if (bullets.Count < maxBullets)
        {
            animator.SetTrigger("loadTrigger");
            bullets.Add(color);
            Tweener.Invoke((reloadTime - 0.3f) / maxBullets, () => LoadBullet(RGBColor.NONE));
        }
        else
        {
            animator.SetTrigger("doneLoadingTrigger");
            Tweener.Invoke(0.2f, () => {
                isReloading = false;
                animator.SetBool("canLoad", isReloading);
            });
           
        }
        
    }
    
    public Vector3 GetPlayerPos () {
        return GameObject.FindGameObjectWithTag("Player").transform.position;
    }

}
