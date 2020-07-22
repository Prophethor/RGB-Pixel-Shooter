﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "TestRevolver", menuName = "Weapons/TestRevolver")]
public class TestRevolver : Weapon {

    public Rigidbody2D bulletPrefab;
    public AudioClip shootEffect;
    public AudioClip reloadStartSFX;
    public AudioClip reloadEndSFX;
    public int dmgAmount = 100;
    public float reloadTime = 3;

    private List<RGBColor> bullets;
    private int maxBullets = 6;
    public float bulletSpeed = 15;
    private GameObject player;

    public AudioClip redInfuseSFX;
    public AudioClip greenInfuseSFX;
    public AudioClip blueInfuseSFX;


    public override string GetName () { return "Revolver"; }

    public override ItemToken GetToken () {
        return new RevolverToken(this);
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
            switch (color)
            {
                case RGBColor.RED:
                    AudioManager.GetInstance().PlaySound(redInfuseSFX, true);
                    break;
                case RGBColor.GREEN:
                    AudioManager.GetInstance().PlaySound(greenInfuseSFX, true);
                    break;
                case RGBColor.BLUE:
                    AudioManager.GetInstance().PlaySound(blueInfuseSFX, true);
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
                player.GetComponent<Animator>().SetTrigger("shootTrigger");
                InstantiateBullet(player.transform.position + (Vector3) deltaPosition);
                bullets.RemoveAt(0);

                AudioManager.GetInstance().PlaySound(shootEffect, true);
            }
            if (bullets.Count == 0) {
                isReloading = true;
                Tweener.Invoke(0.15f, () => {
                    player.GetComponent<Animator>().SetTrigger("loadTrigger");
                    AudioManager.GetInstance().PlaySound(reloadStartSFX);
                    player.GetComponent<TestPlayer>().allPurposeAudio = reloadEndSFX;
                    Tweener.Invoke(reloadTime, () => {
                        Reload();
                        isReloading = false;
                    });
                });
            }
        }
    }

    private void Reload()
    {
        while (bullets.Count < maxBullets) bullets.Add(RGBColor.NONE);
    }

    public Vector3 GetPlayerPos () {
        return GameObject.FindGameObjectWithTag("Player").transform.position;
    }

    public void InstantiateBullet (Vector3 position) {
        Rigidbody2D bulletObj = Instantiate(bulletPrefab, (Vector3) deltaPosition + position, Quaternion.Euler(0, 0, 180));
        bulletObj.velocity = new Vector2(bulletSpeed, 0);
        bulletObj.GetComponent<Projectile>().SetDamage(bullets[0], dmgAmount);
    }
}
