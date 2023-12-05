using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Created by Justin
public class SpeedCollectible : MonoBehaviour
{
    public ParticleSystem speedEffect;
    public ParticleSystem speedupEffect;
    public AudioClip collectedClip;


    Rigidbody2D rigidbody2d;

    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }
  
  void OnTriggerEnter2D(Collider2D other)
    {
    RubyController controller = other.GetComponent<RubyController>();
    if (other.gameObject.CompareTag("Player"))
      {
        controller.SpeedUpEnabled();
        Instantiate(speedupEffect, rigidbody2d.position, Quaternion.identity);
        Destroy(gameObject);
        Destroy(rigidbody2d);
        speedEffect.Stop();
        controller.PlaySound(collectedClip);
     }
    }
  

  
  
  
  
  
}
