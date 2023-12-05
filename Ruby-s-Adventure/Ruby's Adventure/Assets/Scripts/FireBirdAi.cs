using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBirdAi : MonoBehaviour
{
    public float speed;
    public float checkRadius;
    public float attackRadius;
    public float enemyProjectileSpeed;
    public GameObject projectilePrefab;
    public float slowDuration = 2.0f; // Duration for which the player will be slowed down

    public bool shouldRotate;

    public LayerMask whatIsEnemy;

    private Transform target;
    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 movement;
    private float originalSpeed; // Store the original speed of the player

    private bool isInChaseRange;
    private bool isInAttackRange;

    public float shootingCooldown = 1.5f;
    private float lastShotTime;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        target = GameObject.FindWithTag("RubyController").transform;
        originalSpeed = speed; // Store the original speed of the player
    }

    private void Update()
    {
        anim.SetBool("isRunning", isInChaseRange);

        isInChaseRange = Physics2D.OverlapCircle(transform.position, checkRadius, whatIsEnemy);
        isInAttackRange = Physics2D.OverlapCircle(transform.position, attackRadius, whatIsEnemy);

        Vector2 dir = target.position - transform.position;
        dir.Normalize();
        movement = dir;
        if (shouldRotate)
        {
            anim.SetFloat("Move X", dir.x);
            anim.SetFloat("Move Y", dir.y);
        }

        if (isInAttackRange)
        {
            ShootAtTarget();
        }
    }

    private void ShootAtTarget()
    {
        if (Time.time > lastShotTime + shootingCooldown)
        {
            if (projectilePrefab != null && target != null)
            {
                GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();

                if (projectileRb != null)
                {
                    Vector2 direction = (target.position - transform.position).normalized;
                    projectileRb.velocity = direction * enemyProjectileSpeed;

                    lastShotTime = Time.time;
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (isInChaseRange && !isInAttackRange)
        {
            MoveCharacter(movement * speed);
        }
        if (isInAttackRange)
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void MoveCharacter(Vector2 dir)
    {
        rb.MovePosition((Vector2)transform.position + (dir * speed * Time.deltaTime));
    }

 

    IEnumerator SlowPlayer()
    {
        speed = 0.5f * originalSpeed; // Reduce the player's speed to half
        yield return new WaitForSeconds(slowDuration);
        speed = originalSpeed; // Reset the player's speed
    }
}