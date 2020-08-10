using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Revolver", menuName = "Weapons/Revolver")]
public class RevolverInfo : WeaponInfo {

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
    private RevolverBarrel barrel;

    public AudioClip redInfuseSFX;
    public AudioClip greenInfuseSFX;
    public AudioClip blueInfuseSFX;


    public override string GetName () {
        return "Revolver";
    }
}
