using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public Sprite redsprite;
    public Sprite greensprite;
    public Sprite bluesprite;
    public float speed = 30f;
    public int damage;
    private RGBColor color;
    private SpriteRenderer spriteRenderer;
    

    private void Start()
    {
        color = GameBehaviour.RandColor();
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (color == RGBColor.Red) spriteRenderer.sprite = redsprite;
        else if (color == RGBColor.Green) spriteRenderer.sprite = greensprite;
        else spriteRenderer.sprite = bluesprite;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy") {
            EnemyBehaviour enemy = collision.gameObject.GetComponent<EnemyBehaviour>();
            if(enemy.color==color)enemy.TakeDamage(damage);
        }
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}
