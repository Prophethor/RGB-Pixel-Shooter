using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DBShotgun", menuName = "Weapons/DBShotgun")]
public class DBShotgunInfo : WeaponInfo {

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
    public float angle = 45;
    private float angleStep = 0;

    private Animator animator;
    private GameObject player;
    private DBShotgunBarrel barrel;

    public AudioClip redInfuseSFX;
    public AudioClip greenInfuseSFX;
    public AudioClip blueInfuseSFX;

    public override string GetName () {
        return "Shotgun";
    }
}
