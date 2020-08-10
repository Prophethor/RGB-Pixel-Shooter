using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Revolver : Weapon {

    private RevolverInfo revolverInfo;

    private int dmgAmount = 100;
    private float reloadTime = 3;
    private float bulletSpeed = 15;

    private List<RGBColor> bullets;
    private int maxBullets = 6;
    private GameObject player;
    private RevolverBarrel barrel;

    public Revolver () {  }

    protected override void LoadWeaponInfo () {
        weaponInfo = revolverInfo = (RevolverInfo) Resources.Load<RevolverInfo>("Data/Weapons/Revolver");

        icon = revolverInfo.GetIcon();
        name = revolverInfo.GetName();

        // Stats should not depend on ScriptableObject
        dmgAmount = revolverInfo.dmgAmount;
        reloadTime = revolverInfo.reloadTime;
        bulletSpeed = revolverInfo.bulletSpeed;
    }

    public List<RGBColor> GetBullets () {
        return bullets;
    }

    public override void LevelStart () {
        player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<Animator>().SetFloat("loadSpeed", 0.4f / reloadTime);
        bullets = new List<RGBColor>();
        while (bullets.Count < maxBullets) {
            bullets.Add(RGBColor.NONE);
        }
        if (UIHooks == null) {
            InitHooks();
        }
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
                    AudioManager.GetInstance().PlaySound(revolverInfo.redInfuseSFX, true);
                    GameObject.FindObjectOfType<RevolverBarrel>().LoadRed();
                    break;
                case RGBColor.GREEN:
                    AudioManager.GetInstance().PlaySound(revolverInfo.greenInfuseSFX, true);
                    GameObject.FindObjectOfType<RevolverBarrel>().LoadGreen();
                    break;
                case RGBColor.BLUE:
                    AudioManager.GetInstance().PlaySound(revolverInfo.blueInfuseSFX, true);
                    GameObject.FindObjectOfType<RevolverBarrel>().LoadBlue();
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
        // ¯\_(ツ)_/¯
        if (barrel == null) {
            barrel = GameObject.FindObjectOfType<RevolverBarrel>();
            barrel.SetRevolver(this);
        }

        if (!isReloading && !player.GetComponent<PlayerController>().isJumping) {
            if (bullets.Count > 0) {
                player.GetComponent<Animator>().SetTrigger("shootTrigger");
                InstantiateBullet(player.transform.position + (Vector3) revolverInfo.deltaPosition);
                bullets.RemoveAt(0);

                AudioManager.GetInstance().PlaySound(revolverInfo.shootEffect, true);
                GameObject.FindObjectOfType<RevolverBarrel>().Shoot();
            }
            if (bullets.Count == 0) {
                isReloading = true;
                Tweener.Invoke(0.15f, () => {
                    player.GetComponent<Animator>().SetTrigger("loadTrigger");
                    AudioManager.GetInstance().PlaySound(revolverInfo.reloadStartSFX);
                    player.GetComponent<PlayerController>().allPurposeAudio = revolverInfo.reloadEndSFX;
                    Tweener.Invoke(reloadTime, () => {
                        barrel.Reload();
                        Reload();
                        isReloading = false;
                    });
                });
            }
        }
    }

    private void Reload () {
        while (bullets.Count < maxBullets) bullets.Add(RGBColor.NONE);
    }

    public Vector3 GetPlayerPos () {
        return GameObject.FindGameObjectWithTag("Player").transform.position;
    }

    public void InstantiateBullet (Vector3 position) {
        Rigidbody2D bulletObj = GameObject.Instantiate(revolverInfo.bulletPrefab, (Vector3) revolverInfo.deltaPosition + position, Quaternion.Euler(0, 0, 180));
        bulletObj.velocity = new Vector2(bulletSpeed, 0);
        bulletObj.GetComponent<Projectile>().SetDamage(bullets[0], dmgAmount);
    }
}
