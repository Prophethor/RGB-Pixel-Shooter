using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Revolver", menuName = "Revolver")]
public class Revolver : Weapon {
    public int dmgAmount = 1;
    public Rigidbody2D bulletPrefab;
    List<RGBColor> bullets;
    public float bulletSpeed = 5;

    public override void LevelStart () {
        bullets = new List<RGBColor>();
    }

    public override string GetName () { return "Revolver"; }

    public void Load (RGBColor color) {
        if (bullets.Count < 6) {
            bullets.Add(color);
        }
    }

    public override void Shoot (Vector3 position) {
        if (bullets.Count > 0) {
            Rigidbody2D bulletObj = Instantiate(bulletPrefab, (Vector3) deltaPosition + position, Quaternion.identity);
            bulletObj.velocity = new Vector2(bulletSpeed, 0);
            bulletObj.GetComponent<Projectile>().SetDamage(bullets[0], dmgAmount);
            bullets.RemoveAt(0);
        }
    }
}
