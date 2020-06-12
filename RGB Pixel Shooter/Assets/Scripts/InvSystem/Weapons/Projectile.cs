using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    [SerializeField]
    public RGBDamage damage;
    public GameObject bloodSplaterSuccessfull;
    public GameObject bloodSplatterUnsuccessfull;
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

            if (damage.color == RGBColor.BLUE)
            {
                bloodSplaterSuccessfull.GetComponent<SpriteRenderer>().color = Color.blue;
            }
            if (damage.color == RGBColor.RED)
            {
                bloodSplaterSuccessfull.GetComponent<SpriteRenderer>().color = Color.red;
            }
            if (damage.color == RGBColor.GREEN)
            {
                bloodSplaterSuccessfull.GetComponent<SpriteRenderer>().color = Color.green;
            }
            
            Instantiate(bloodSplaterSuccessfull, spawnPosition + offsetVector, Quaternion.identity);

        }
        else if (damage.color == RGBColor.NONE)
        {
            Vector3 offsetVector = new Vector3(Random.Range(-.1f, .1f), Random.Range(-.1f, .1f), 0);
            bloodSplatterUnsuccessfull.GetComponent<SpriteRenderer>().color = Color.gray;
            Instantiate(bloodSplatterUnsuccessfull, spawnPosition + offsetVector, Quaternion.identity);
        }
        else
        {
            Vector3 offsetVector = new Vector3(Random.Range(-.1f, .1f), Random.Range(-.1f, .1f), 0);

            if (damage.color == RGBColor.BLUE)
            {
                bloodSplatterUnsuccessfull.GetComponent<SpriteRenderer>().color = Color.blue;
            }
            if (damage.color == RGBColor.RED)
            {
                bloodSplatterUnsuccessfull.GetComponent<SpriteRenderer>().color = Color.red;
            }
            if (damage.color == RGBColor.GREEN)
            {
                bloodSplatterUnsuccessfull.GetComponent<SpriteRenderer>().color = Color.green;
            }
           
            Instantiate(bloodSplatterUnsuccessfull, spawnPosition + offsetVector, Quaternion.identity);
        }
    

    }
}
