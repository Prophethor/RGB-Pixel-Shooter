using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TestShotgun", menuName = "Weapons/TestShotgun")]
public class TestShotgun : Weapon {

    public Rigidbody2D bulletPrefab;
    public AudioClip shootEffect;
    public AudioClip reloadStartSFX;
    public AudioClip reloadEndSFX;
    public int dmgAmount = 100;
    public float reloadTime = 3;

    private List<RGBColor> bullets;
    private int maxBullets = 2;
    public float bulletSpeed = 15;
    public int numberOfPellets = 5;
    public float range = 5;
    public double angle = 45;
    private double angleStep = 0;

    private Animator animator;
    private GameObject player;

    public AudioClip redInfuseSFX;
    public AudioClip greenInfuseSFX;
    public AudioClip blueInfuseSFX;

    public override string GetName () { return "Shotgun"; }

    public List<RGBColor> GetBullets () {
        return bullets;
    }

    public override ItemToken GetToken () {
        return new ShotgunToken(this);
    }

    public override void LevelStart () {
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
            switch (color)
            {
                case RGBColor.RED:
                    AudioManager.GetInstance().PlaySound(redInfuseSFX, true);
                    FindObjectOfType<DBShotgunBarrel>().LoadRed();
                    break;
                case RGBColor.GREEN:
                    AudioManager.GetInstance().PlaySound(greenInfuseSFX, true);
                    FindObjectOfType<DBShotgunBarrel>().LoadGreen();
                    break;
                case RGBColor.BLUE:
                    AudioManager.GetInstance().PlaySound(blueInfuseSFX, true);
                    FindObjectOfType<DBShotgunBarrel>().LoadBlue();
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
        if (!isReloading && !player.GetComponent<TestPlayer>().isJumping) {
            if (bullets.Count > 0) {
                animator.SetTrigger("shootTrigger");
                if (numberOfPellets != 1) {
                    for (int i = 0; i < numberOfPellets; i++) {
                        InstantiatePellet(player.transform.position + (Vector3) deltaPosition, angle - i * angleStep);
                    }
                }
                else {
                    InstantiatePellet(player.transform.position + (Vector3) deltaPosition, 0);
                }
                bullets.RemoveAt(0);
                AudioManager.GetInstance().PlaySound(shootEffect, true);
                FindObjectOfType<DBShotgunBarrel>().Shoot();
            }
            if (bullets.Count == 0) {
                isReloading = true;
                Tweener.Invoke(0.15f, () => {
                    animator.SetTrigger("loadTrigger");
                    AudioManager.GetInstance().PlaySound(reloadStartSFX);
                    player.GetComponent<TestPlayer>().allPurposeAudio = reloadEndSFX;
                    Tweener.Invoke(reloadTime, () => {
                        Reload();
                        FindObjectOfType<DBShotgunBarrel>().Reload();
                        isReloading = false;
                    });
                });
            }
        }
    }

    private void Reload () {
        while (bullets.Count < maxBullets) bullets.Add(RGBColor.NONE);
    }

    private void InstantiatePellet (Vector3 position, double angle) {
        //why does it need to be angle-180 and not just angle??
        Rigidbody2D bulletObj = Instantiate(bulletPrefab, (Vector3) deltaPosition + position, Quaternion.Euler(0, 0, (float) angle - 180));
        bulletObj.velocity = new Vector2((float) Math.Cos(angle * Mathf.Deg2Rad) * bulletSpeed, (float) Math.Sin(angle * Mathf.Deg2Rad) * bulletSpeed);
        bulletObj.GetComponent<Projectile>().SetDamage(bullets[0], dmgAmount);
        bulletObj.GetComponent<Projectile>().SetRange(range);
    }

    public Vector3 GetPlayerPos () {
        return GameObject.FindGameObjectWithTag("Player").transform.position;
    }

}
