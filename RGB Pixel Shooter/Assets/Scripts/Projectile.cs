using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    [SerializeField]
    public RGBDamage damage;

    // Array of bullet sprites in order: Red, Green, Blue
    public Sprite[] bulletSprites = new Sprite[3];

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
}
