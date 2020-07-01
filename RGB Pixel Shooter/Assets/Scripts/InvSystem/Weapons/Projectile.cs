using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    [SerializeField]
    public RGBDamage damage;
    public BulletPopScript bloodPop;
    
    // Array of bullet sprites in order: Red, Green, Blue
    public Sprite[] bulletSprites = new Sprite[3];

    public void Awake()
    {
        bloodPop = FindObjectOfType<BulletPopScript>();
    }

    private void Update () {
        if (transform.position.magnitude > 100f) {
            Destroy(gameObject);
        }
    }

    public void SetDamage (RGBColor color, int amount) {
       
            damage.color = color;
            damage.amount = amount;
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            sr.sprite = bulletSprites[(int)color];

    }

    public void SpawnBloodSplater (Vector3 spawnPosition, HitStatus data, RGBColor enemyColor)
    {
        bool nullhit;
        Vector3 offsetVector = new Vector3(Random.Range(-.1f, .1f), Random.Range(-.1f, .1f), 0);
        if (data.belowThreshold && (data.damageAmount <= 0))
        {
            nullhit = true;
        }
        else nullhit = false;

        bloodPop.SpawnProjectile(spawnPosition + offsetVector, damage.color, data.hitColor, nullhit);

    }
}
