﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    [SerializeField]
    public RGBDamage damage;
    public GameObject bloodSplatter;
    // Array of bullet sprites in order: Red, Green, Blue
    public Sprite[] bulletSprites = new Sprite[4];

    private void Update () {
        if (transform.position.magnitude > 100f) {
            Destroy(gameObject);
        }
    }

    public void SetDamage (RGBColor color, int amount) {
        damage.color = color;
        damage.amount = amount;
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.sprite = bulletSprites[(int) color];
    }

    public void SpawnBloodSplater (Vector3 spawnPosition) {
        Vector3 offsetVector = new Vector3(Random.Range(-.1f, .1f), Random.Range(-.1f, .1f), 0);

        GameObject splatterObject = Instantiate(bloodSplatter, spawnPosition + offsetVector, Quaternion.identity);

        splatterObject.GetComponent<SpriteRenderer>().color = damage.color.GetColor();
    }
}
