using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public float speed;
    public bool vertical;
    public float changeTime = 3.0f;
    public int score = 0;
    public Text scoreText;

    Rigidbody2D rigidbody2d;
    float timer;
    int direction = 1;

    Animator animator;

    bool broken = true;

    public ParticleSystem smokeEffect;
    private RubyController rubyController;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        timer = changeTime;
        animator = GetComponent<Animator>();
       

        GameObject rubyControllerObject = GameObject.FindWithTag("RubyController"); //this line of code finds the RubyController script by looking for a "RubyController" tag on Ruby

        if (rubyControllerObject != null)
        {

            rubyController = rubyControllerObject.GetComponent<RubyController>(); //and this line of code finds the rubyController and then stores it in a variable

            print("Found the RubyConroller Script!");

        }
        if (rubyController == null)

        {

            print("Cannot find GameController Script!");

        }
    }

    // Update is called once per frame
    void Update()
    {
        //remember ! inverse the test, so if broken is true !broken will be false and return won't be executed.
        if(!broken)
        {
            return;
        }

        timer -= Time.deltaTime;

        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }
    }

    void FixedUpdate()
    {
        if(!broken)
        {
           return;
        }
        Vector2 position = rigidbody2d.position;

        if (vertical)
        {
            position.y = position.y + Time.deltaTime * speed * direction;

            animator.SetFloat("Move X", 0);
            animator.SetFloat("Move Y", direction);
        }
        else
        {
             position.x = position.x + Time.deltaTime * speed * direction;

             animator.SetFloat("Move X", direction);
             animator.SetFloat("Move Y", 0);
        }
       
        rigidbody2d.MovePosition(position);

        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        RubyController player = other.gameObject.GetComponent<RubyController>();

        if (player != null)
        {
            player.ChangeHealth(-1);
        }
    }

    //Public because we want to call it from elsewhere like the projectile script
    public void Fix()
    {
        smokeEffect.Stop();
        broken = false;
        rigidbody2d.simulated = false;
        animator.SetTrigger("Fixed");
        ScoreManager.scoreCount += 1;


    }

}
