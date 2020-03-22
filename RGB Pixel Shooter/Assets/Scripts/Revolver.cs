using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revolver : MonoBehaviour
{
    public Rigidbody2D projectile;
    public int damage = 1;
    public float range = 0.9f;
    public int maxBullets = 6;
    public List<RGBColor> bullets = new List<RGBColor>();
    private int bulletsLoaded = 0;
    private int loadIndex = 0;
    private int shootIndex = 0;

    private void Start()
    {
        for (int i = 0; i < maxBullets; i++) bullets.Add(RGBColor.None);
    }
    public void LoadRed()
    {
        if (bulletsLoaded < maxBullets)
        {
            loadIndex = shootIndex;
            while (bullets[loadIndex] != RGBColor.None) loadIndex = (loadIndex + 1) % maxBullets;
            bullets[loadIndex] = RGBColor.Red;
            bulletsLoaded++;
        }
    }

    public void LoadBlue()
    {
        if (bulletsLoaded < maxBullets)
        {
            loadIndex = shootIndex;
            while (bullets[loadIndex] != RGBColor.None) loadIndex = (loadIndex + 1) % maxBullets;
            bullets[loadIndex] = RGBColor.Blue;
            bulletsLoaded++;
        }
    }

    public void LoadGreen()
    {
        if (bulletsLoaded < maxBullets)
        {
            loadIndex = shootIndex;
            while (bullets[loadIndex] != RGBColor.None) loadIndex = (loadIndex + 1) % maxBullets;
            bullets[loadIndex] = RGBColor.Green;
            bulletsLoaded++;
        }
    }
    public void Shoot()
    {
        if (bulletsLoaded > 0 && bullets[shootIndex]!=RGBColor.None) {
            SpawnBullet();
            bullets[shootIndex] = RGBColor.None;
            shootIndex = (shootIndex + 1) % maxBullets;
            bulletsLoaded--;
        } else {
            shootIndex = (shootIndex + 1) % maxBullets;
        }
    }

    private void SpawnBullet()
    {
        Rigidbody2D p = Instantiate(projectile, transform.position, Quaternion.identity);
        p.velocity = new Vector3(1, 0, 0) * p.GetComponent<Bullet>().speed;
        p.GetComponent<Bullet>().damage = damage;
        p.GetComponent<Bullet>().color = bullets[shootIndex];
        p.GetComponent<Bullet>().range = range;
    }

    private void Update()
    {
        foreach(Transform bullet in transform)
        {
            if (bullet.position.x >= 2 * Camera.main.transform.position.x * range) Destroy(bullet.gameObject);
        }
    }
}
