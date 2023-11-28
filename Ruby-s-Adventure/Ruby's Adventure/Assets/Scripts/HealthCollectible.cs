using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    public ParticleSystem healthEffect;
    public ParticleSystem healingEffect;
    public AudioClip collectedClip;

    Rigidbody2D rigidbody2d;

     void Start()
 {
     rigidbody2d = GetComponent<Rigidbody2D>();
 }

    void OnTriggerEnter2D(Collider2D other)
    {
        RubyController controller = other.GetComponent<RubyController>();

        if (controller != null)
        {
            if(controller.health < controller.maxHealth)
            {
                 Instantiate(healingEffect, rigidbody2d.position, Quaternion.identity);
                 controller.ChangeHealth(1);
                 Destroy(gameObject);
                 Destroy(rigidbody2d);
                 healthEffect.Stop();
                 controller.PlaySound(collectedClip);
                 
            }
        }
    }
}

