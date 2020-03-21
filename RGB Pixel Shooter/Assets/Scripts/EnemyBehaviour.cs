using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public float speed = 10;
    public int health = 1;
    public RGBColor color;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity -= new Vector2(speed,0);
        color = GameBehaviour.RandColor();
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (color == RGBColor.Red) spriteRenderer.color = Color.red;
        else if (color == RGBColor.Green) spriteRenderer.color = Color.green;
        else spriteRenderer.color = Color.blue;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Die();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
