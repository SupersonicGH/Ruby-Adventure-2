using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 3f;
    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 movementDirection = Vector2.zero;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        // Start the movement coroutine
        StartCoroutine(EnemyMovementCoroutine());
    }

    IEnumerator EnemyMovementCoroutine()
    {
        while (true)
        {
            // Randomly choose a direction
            int randomDirection = Random.Range(0, 5); // 0: up, 1: down, 2: left, 3: right, 4: idle

            // Set the movement direction and animation based on the random direction
            switch (randomDirection)
            {
                case 0:
                    movementDirection = Vector2.up;
                    break;
                case 1:
                    movementDirection = Vector2.down;
                    break;
                case 2:
                    movementDirection = Vector2.left;
                    break;
                case 3:
                    movementDirection = Vector2.right;
                    break;
                case 4:
                    movementDirection = Vector2.zero;
                    break;
            }

            // Set animation parameters
            animator.SetFloat("Move X", movementDirection.x);
            animator.SetFloat("Move Y", movementDirection.y);

            // Move the enemy
            rb.velocity = movementDirection * moveSpeed;

            // Wait for a few seconds before changing direction
            yield return new WaitForSeconds(2f);
        }
    }
}