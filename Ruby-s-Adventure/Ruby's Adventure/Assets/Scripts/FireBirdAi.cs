using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireBirdAi : MonoBehaviour
{
    //Alexander Thompson made this
    public float speed;
    public float checkRadius;
    public float attackRadius;
    public float enemyProjectileSpeed;
    public GameObject projectilePrefab;

    public bool shouldRotate;

    public LayerMask whatIsEnemy;

    private Transform target;
    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 movement;
    public Vector3 dir;

    private bool isInChaseRange;
    private bool isInAttackRange;

    private float lastShotTime;
    public float shootingCooldown = 1.5f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        target = GameObject.FindWithTag("RubyController").transform;

        lastShotTime = -shootingCooldown; // To allow shooting immediately upon starting
    }

    private void Update()
    {
        anim.SetBool("isRunning", isInChaseRange);

        isInChaseRange = Physics2D.OverlapCircle(transform.position, checkRadius, whatIsEnemy);
        isInAttackRange = Physics2D.OverlapCircle(transform.position, attackRadius, whatIsEnemy);

        dir = target.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
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
                }

                Destroy(projectile, 3f); // Destroy the projectile after 3 seconds

                lastShotTime = Time.time; // Update the last shot time
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

   
}
