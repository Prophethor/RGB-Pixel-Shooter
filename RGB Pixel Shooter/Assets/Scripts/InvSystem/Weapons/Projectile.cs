using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    [SerializeField]
    public RGBDamage damage;

    public GameObject bloodPop;

    public Animator animator;

    
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
        //Delete this when you get sprite for uncolored bullets
        if (color == RGBColor.NONE) sr.color = Color.black;
    }


}
