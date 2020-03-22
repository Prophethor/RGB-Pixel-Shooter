using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Sprite redsprite;
    public Sprite greensprite;
    public Sprite bluesprite;
    public float speed = 30f;
    public int damage;
    public float range;
    public RGBColor color;
    private SpriteRenderer spriteRenderer;
    

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (color == RGBColor.Red) spriteRenderer.sprite = redsprite;
        else if (color == RGBColor.Green) spriteRenderer.sprite = greensprite;
        else spriteRenderer.sprite = bluesprite;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy") {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy.color == color)
            {
                enemy.TakeDamage(damage);
                GameBehaviour.enemiesToKill--;
            }
        }
        Destroy(gameObject);
    }

    private void Update()
    {
        if (transform.position.x >= 2 * Camera.main.transform.position.x * range) Destroy(gameObject);
    }
}
