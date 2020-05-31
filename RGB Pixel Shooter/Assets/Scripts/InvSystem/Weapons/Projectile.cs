using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    [SerializeField]
    public RGBDamage damage;
    public GameObject bloodSplater; 
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

    public void SpawnBloodSplater (Vector3 spawnPosition)
    {
        Vector3 offsetVector = new Vector3(Random.Range(-.1f, .1f), Random.Range(-.1f, .1f), 0);

        if (damage.color == RGBColor.BLUE)
        {
            bloodSplater.GetComponent<SpriteRenderer>().color = Color.blue;
        }
        if (damage.color == RGBColor.RED)
        {
            bloodSplater.GetComponent<SpriteRenderer>().color = Color.red;
        }
        if (damage.color == RGBColor.GREEN)
        {
            bloodSplater.GetComponent<SpriteRenderer>().color = Color.green;
        }
        /*
        switch (color)
        {
            case RGBColor.RED:
                bloodSplater.GetComponent<SpriteRenderer>().color = Color.red;
                break;
            case RGBColor.GREEN:
                bloodSplater.GetComponent<SpriteRenderer>().color = Color.green;
                break;
            case RGBColor.BLUE:
                bloodSplater.GetComponent<SpriteRenderer>().color = Color.blue;
                break;
            default:
                break;
                 }*/
        Instantiate(bloodSplater, spawnPosition + offsetVector, Quaternion.identity);
   

    }
}
