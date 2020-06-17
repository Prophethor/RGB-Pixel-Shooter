using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    [SerializeField]
    public RGBDamage damage;
    public GameObject bloodPop;

    public Animator animator;

    
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

    public void SpawnBloodSplater (Vector3 spawnPosition, HitStatus data, RGBColor enemyColor)
    {
        Vector3 offsetVector = new Vector3(Random.Range(-.1f, .1f), Random.Range(-.1f, .1f), 0);
        Instantiate(bloodPop, spawnPosition + offsetVector, Quaternion.identity);
        Animator tempAnim = bloodPop.GetComponent<Animator>();
        bloodPop.GetComponent<BloodSplatterTestScript>().Initialize(enemyColor, data);

        if (data.hitColor == HitColor.CORRECT)
        {
            tempAnim.SetTrigger("crit");
           

        }
        if ((data.hitColor == HitColor.NEUTRAL) && (data.hitColor == HitColor.WRONG))
        {
            tempAnim.SetTrigger("neutral");
           
        }

        if (data.damageAmount <= 0 && data.belowThreshold)
        {
            tempAnim.SetTrigger("null");
        }

        


    }
}
