using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class DBShotgun : Weapon {

    [System.NonSerialized]
    private DBShotgunInfo shotgunInfo;

    private int dmgAmount = 100;
    private float reloadTime = 3;

    private int numberOfPellets = 3;
    private float bulletSpeed = 15;
    private float range;
    private float angle;
    private float angleStep = 0;

    private List<RGBColor> bullets;
    private const int maxBullets = 2;

    private Animator animator;
    private GameObject player;
    private DBShotgunBarrel barrel;

    public DBShotgun () { }

    protected override void LoadWeaponInfo () {
        weaponInfo = shotgunInfo = (DBShotgunInfo) Resources.Load<DBShotgunInfo>("Data/Weapons/DBShotgun");

        icon = shotgunInfo.GetIcon();
        name = shotgunInfo.GetName();

        dmgAmount = shotgunInfo.dmgAmount;
        reloadTime = shotgunInfo.reloadTime;

        numberOfPellets = shotgunInfo.numberOfPellets;
        bulletSpeed = shotgunInfo.bulletSpeed;
        range = shotgunInfo.range;
        angle = shotgunInfo.angle;
    }

    public List<RGBColor> GetBullets () {
        return bullets;
    }

    public override void LevelStart () {
        LoadWeaponInfo();

        player = GameObject.FindGameObjectWithTag("Player");
        animator = player.GetComponent<Animator>();
        animator.SetFloat("loadSpeed", 0.25f / reloadTime);
        bullets = new List<RGBColor>();
        Reload();
        if (UIHooks == null) {
            InitHooks();
        }
        if (numberOfPellets != 1) angleStep = angle * 2 / (numberOfPellets - 1);
    }

    protected override void InitHooks () {
        UIHooks = new Dictionary<string, UnityEngine.Events.UnityAction>();
        UIHooks.Add("LoadRed", () => Load(RGBColor.RED));
        UIHooks.Add("LoadGreen", () => Load(RGBColor.GREEN));
        UIHooks.Add("LoadBlue", () => Load(RGBColor.BLUE));
        UIHooks.Add("Shoot", () => Shoot());
    }

    public override void Load (RGBColor color) {
        for (int i = 0; i < bullets.Count; i++) {
            if (bullets[i] != RGBColor.NONE) continue;
            switch (color) {
                case RGBColor.RED:
                    AudioManager.GetInstance().PlaySound(shotgunInfo.redInfuseSFX, true);
                    GameObject.FindObjectOfType<DBShotgunBarrel>().LoadRed();
                    break;
                case RGBColor.GREEN:
                    AudioManager.GetInstance().PlaySound(shotgunInfo.greenInfuseSFX, true);
                    GameObject.FindObjectOfType<DBShotgunBarrel>().LoadGreen();
                    break;
                case RGBColor.BLUE:
                    AudioManager.GetInstance().PlaySound(shotgunInfo.blueInfuseSFX, true);
                    GameObject.FindObjectOfType<DBShotgunBarrel>().LoadBlue();
                    break;
                case RGBColor.NONE:
                    break;
                default:
                    break;
            }
            bullets[i] = color;
            break;
        }
    }

    public override void Shoot () {
        if (barrel == null) {
            barrel = GameObject.FindObjectOfType<DBShotgunBarrel>();
            barrel.SetShotgun(this);
        }

        if (!isReloading && !player.GetComponent<PlayerController>().isJumping) {
            if (bullets.Count > 0) {
                animator.SetTrigger("shootTrigger");
                if (numberOfPellets != 1) {
                    for (int i = 0; i < numberOfPellets; i++) {
                        InstantiatePellet(player.transform.position + (Vector3) shotgunInfo.deltaPosition, angle - i * angleStep);
                    }
                }
                else {
                    InstantiatePellet(player.transform.position + (Vector3) shotgunInfo.deltaPosition, 0);
                }
                bullets.RemoveAt(0);
                AudioManager.GetInstance().PlaySound(shotgunInfo.shootEffect, true);
                GameObject.FindObjectOfType<DBShotgunBarrel>().Shoot();
            }
            if (bullets.Count == 0) {
                isReloading = true;
                Tweener.Invoke(0.15f, () => {
                    animator.SetTrigger("loadTrigger");
                    AudioManager.GetInstance().PlaySound(shotgunInfo.reloadStartSFX);
                    player.GetComponent<PlayerController>().allPurposeAudio = shotgunInfo.reloadEndSFX;
                    Tweener.Invoke(reloadTime, () => {
                        Reload();
                        barrel.Reload();
                        isReloading = false;
                    });
                });
            }
        }
    }

    private void Reload () {
        while (bullets.Count < maxBullets) bullets.Add(RGBColor.NONE);
    }

    private void InstantiatePellet (Vector3 position, float angle) {
        //why does it need to be angle-180 and not just angle??
        Rigidbody2D bulletObj = GameObject.Instantiate(shotgunInfo.bulletPrefab, (Vector3) shotgunInfo.deltaPosition + position, Quaternion.Euler(0, 0, (float) angle - 180));
        bulletObj.velocity = new Vector2((float) Mathf.Cos(angle * Mathf.Deg2Rad) * bulletSpeed, (float) Mathf.Sin(angle * Mathf.Deg2Rad) * bulletSpeed);
        bulletObj.GetComponent<Projectile>().SetDamage(bullets[0], dmgAmount);
        bulletObj.GetComponent<Projectile>().SetRange(range);
    }

    public Vector3 GetPlayerPos () {
        return GameObject.FindGameObjectWithTag("Player").transform.position;
    }
}
