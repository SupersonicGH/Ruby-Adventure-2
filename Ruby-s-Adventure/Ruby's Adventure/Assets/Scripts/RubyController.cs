using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RubyController : MonoBehaviour
{
    public float speed = 3.0f;
    public float speedBoost = 5.0f;
    bool speedUp;

    public int maxHealth = 5;
    public float timeInvincible = 2.0f;

    public GameObject projectilePrefab;
    public GameObject JambiAudio;

    int currentHealth;
    public int health { get { return currentHealth; }}
    
    bool isInvincible;
    float invincibleTimer;

    Rigidbody2D rigidbody2d;
    float horizontal;
    float vertical;

    Animator animator;
    Vector2 lookDirection = new Vector2(1,0);

    AudioSource audiosource;
    public AudioClip throwClip;
    public AudioClip hitClip;
    public AudioClip JambiClip;

    public ParticleSystem hitEffect;

    
    public Text GameOverText;
    public GameManagerScript gameManager;
    public GameManagerScript restartR;
    // public GameObject winTextObject;

    private bool isDead;
    private bool gameOver;
   


    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();  
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        audiosource = GetComponent<AudioSource>();
        restartR = FindObjectOfType<GameManagerScript>();

      

    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

       Vector2 move = new Vector2(horizontal, vertical);

    

       if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
       {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
       }

       animator.SetFloat("Look X", lookDirection.x);
       animator.SetFloat("Look Y", lookDirection.y);
       animator.SetFloat("Speed", move.magnitude);

        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }

        if(Input.GetKeyDown(KeyCode.C))
        {
            Launch();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
             RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));
            if (hit.collider != null)
            {
                
                NonPlayerCharacter character = hit.collider.GetComponent<NonPlayerCharacter>();
                if (character != null)
                {

                    PlaySound(JambiClip);//Alex added 
                    character.DisplayDialog();
                }  
            }
        }
        if (currentHealth <= 0 && !isDead) 
        {
            isDead = true;
            gameManager.gameOver();
            Debug.Log("Dead");
            speed = 0.0f;
            

        }
        


    }


    void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;
        
        rigidbody2d.MovePosition(position);
    }

   /* public void ChangeScore(int scoreAmount)
    {
        score += scoreAmount;

    }*/

    public void ChangeHealth(int amount)
    {
            if (amount < 0)
            {
                Instantiate(hitEffect, rigidbody2d.position, Quaternion.identity);
                animator.SetTrigger("Hit");
                PlaySound(hitClip);
                if (isInvincible)
                    return;

                isInvincible = true;
                invincibleTimer = timeInvincible;    
            }


        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);
    }

    void Launch()
    {
         GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);

        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDirection, 300);

        animator.SetTrigger("Launch");
        PlaySound(throwClip);
    }

    public void PlaySound(AudioClip clip)
    {
        audiosource.PlayOneShot(clip);
    }

    //added by Justin
    public void SpeedUpEnabled()
    {
        speedUp = true;
        speed *= speedBoost;
        StartCoroutine (SpeedUpDisableRoutine());
    }

    IEnumerator SpeedUpDisableRoutine()
    {
        yield return new WaitForSeconds(3.0f);

        speed /= speedBoost;
    }
}
