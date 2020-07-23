using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    [SerializeField]
    public RGBDamage damage;
    public GameObject bloodSplatter;

    private float range = 100f;
    private Vector3 startPosition;

    public AudioClip hitSFX;

    private Animator animator;

    private void Start () {
        startPosition = transform.position;
        animator = this.gameObject.GetComponent<Animator>();
    }

    private void Update () {
        if ((transform.position-startPosition).magnitude > range) {
            Destroy(gameObject);
        }
    }

    public void SetDamage (RGBColor color, int amount) {
        damage.color = color;
        damage.amount = amount;
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.color = color.GetColor();
    }

    public void SetRange(float range) {
        this.range = range;
    }

    public void SpawnBloodSplater (Vector3 spawnPosition, RGBColor bulletColor, RGBColor enemyColor = RGBColor.NONE) {

       

        Vector3 offsetVector = new Vector3(Random.Range(-.1f, .1f), Random.Range(-.1f, .1f), 0);

        GameObject splatterObject = Instantiate(bloodSplatter, spawnPosition + offsetVector, Quaternion.identity);
        splatterObject.GetComponent<SpriteRenderer>().color = damage.color.GetColor();

        Animator explosionAnimator = splatterObject.GetComponent<Animator>();
        if (bulletColor == enemyColor)
        {
            explosionAnimator.SetInteger("ExplosionInt", 2);
        }
        else
        {
            explosionAnimator.SetInteger("ExplosionInt", 1);
        }

        Tweener.Invoke(.3f, () => { Destroy(splatterObject); });
    }

    public void SelfDestruct()
    {
        AudioManager.GetInstance().PlaySound(hitSFX, true);
        Destroy(this.gameObject);    }
}
