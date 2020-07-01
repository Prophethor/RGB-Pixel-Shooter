﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TestShotgun", menuName = "Weapons/TestShotgun")]
public class TestShotgun : Weapon {

    public Rigidbody2D bulletPrefab;
    public int dmgAmount = 100;
    public float reloadTime = 3;

    private List<RGBColor> bullets;
    private int maxBullets = 2;
    private float bulletSpeed = 15;
    [HideInInspector]
    public bool isReloading = false;
    public int numberOfPellets = 5;
    public float range = 5;
    public double angle = 45;
    private double angleStep = 0;

    private Animator animator;
    private GameObject player;

    public override string GetName () { return "Shotgun"; }

    public override void LevelStart () {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = player.GetComponent<Animator>();
        animator.SetFloat("loadSpeed", 0.4f / reloadTime);
        bullets = new List<RGBColor>();
        Reload();
        if (UIHooks == null) {
            InitHooks();
        }
        if(numberOfPellets!=1)angleStep = angle * 2 / (numberOfPellets-1);
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
                if (numberOfPellets != 1) {
                    for (int i = 0; i < numberOfPellets; i++) {
                        InstantiatePellet(position, angle - i * angleStep);
                    }
                }
                else {
                    InstantiatePellet(position, 0);
                }
                bullets.RemoveAt(0);
            }
            if (bullets.Count == 0) {
                isReloading = true;
                Tweener.Invoke(0.15f, () => {
                    animator.SetTrigger("loadTrigger");
                    Tweener.Invoke(reloadTime, () => {
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

    private void InstantiatePellet (Vector3 position, double angle) {
        Rigidbody2D bulletObj = Instantiate(bulletPrefab, (Vector3) deltaPosition + position, Quaternion.identity);
        Debug.Log(angle);
        bulletObj.velocity = new Vector2((float)Math.Cos(angle*Mathf.Deg2Rad)*bulletSpeed, (float) Math.Sin(angle*Mathf.Deg2Rad)*bulletSpeed);
        bulletObj.GetComponent<Projectile>().SetDamage(bullets[0], dmgAmount);
        bulletObj.GetComponent<Projectile>().SetRange(range);
    }

    public Vector3 GetPlayerPos () {
        return GameObject.FindGameObjectWithTag("Player").transform.position;
    }

}