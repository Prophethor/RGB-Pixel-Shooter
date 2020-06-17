using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour {

    [SerializeField]
    public RGBDamage damage;
    public GameObject bloodPop;

    public Animator animator;

    public AnimatorOverrideController animRed;
    public AnimatorOverrideController animBlue;
    public AnimatorOverrideController animGreen;

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
            sr.sprite = bulletSprites[(int)color];

    }

    public void SpawnBloodSplater (Vector3 spawnPosition, bool rightColor)
    {
        if (rightColor)
        {
            Vector3 offsetVector = new Vector3(Random.Range(-.1f, .1f), Random.Range(-.1f, .1f), 0);

            
            
            Instantiate(bloodPop, spawnPosition + offsetVector, Quaternion.identity);

        }
        else if (damage.color == RGBColor.NONE)
        {
            Vector3 offsetVector = new Vector3(Random.Range(-.1f, .1f), Random.Range(-.1f, .1f), 0);
            Instantiate(bloodPop, spawnPosition + offsetVector, Quaternion.identity);
        }
       
    

    }
}
