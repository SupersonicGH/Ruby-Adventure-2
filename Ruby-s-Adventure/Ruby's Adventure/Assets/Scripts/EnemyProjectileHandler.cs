using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyProjectileHandler : MonoBehaviour
{
    //Alex Thompson Made this code
    public float originalSpeed = 5f; // Change this to the player's original speed
    public float reducedSpeed = 2.5f; // Set the reduced speed after collision
    private float speedTimer = 0f;
   

    private RubyController rubyController; // Reference to the RubyController script

    private void Start()
    {
        rubyController = GameObject.FindWithTag("RubyController").GetComponent<RubyController>();
        
    }

    private void Update()
    {
        if (speedTimer > 0)
        {
            speedTimer -= Time.deltaTime;
            if (speedTimer <= 0)
            {
                // Restore original speed after the timer expires
                rubyController.speed = originalSpeed;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("RubyController"))
        {
            RubyController rubyController = other.gameObject.GetComponent<RubyController>();
            if (rubyController != null)
            {
                // Reduce player speed and start a timer
               

                rubyController.ReduceSpeedForDuration(reducedSpeed, 2f); // Change 'reducedSpeed' and duration as needed

                // Destroy the projectile on hitting the player
                Destroy(gameObject);
                
            }
        }
    }

    


}