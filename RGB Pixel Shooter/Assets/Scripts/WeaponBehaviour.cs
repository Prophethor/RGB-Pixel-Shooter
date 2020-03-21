using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBehaviour : MonoBehaviour
{
    public Rigidbody2D projectile;
    public int bullets = 0;
    public int maxBullets = 6;
    public int damage = 1;
    public float range = 0.9f;
    public void Load()
    {
        if(bullets<maxBullets)bullets++;
    }

    public void Shoot()
    {
        if (bullets > 0) {
            Rigidbody2D p = Instantiate(projectile, transform.position, Quaternion.identity);
            p.velocity = new Vector3(1,0,0) * p.GetComponent<BulletBehaviour>().speed;
            p.GetComponent<BulletBehaviour>().damage = damage;
            bullets--;
        }
    }
}
