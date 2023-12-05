using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileController : MonoBehaviour
{
    private Rigidbody2D rb;
    private float speed;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Launch the projectile in a specific direction and speed
    public void Launch(Vector2 direction, float projectileSpeed)
    {
        speed = projectileSpeed;
        rb.velocity = direction * speed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // If the projectile collides with something, destroy the projectile
        Destroy(gameObject);
    }
}