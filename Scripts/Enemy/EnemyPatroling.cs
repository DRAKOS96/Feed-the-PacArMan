using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using System;

public class EnemyPatroling : MonoBehaviour
{
    
    public ScriptManager scriptManager;

    //Destination Agent
    public Transform[] points;
    private int destPoint = 0;
    private NavMeshAgent agent;

    public NavMeshSurface surface;

    //Animator
    Animator animator;
    int animOnce = 0;


    public AudioClip GameOverSound;

    bool timeISoff = false;

    //Canvas
    public GameObject GameOverCanvas;
    public GameObject ScoreCanvas;

    //Gia na exei syndesh me alla script
    public static EnemyPatroling instance;

    //Initialization Health
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;

    //Check - 2 Ti na trexei
    public int checkEnemy;
    public AudioClip HitSound;
    public AudioClip DestroyEnemy;
    public SuperPowerEnable superPowerEnable;

    //Destination Home
    public Transform[] HomePoints;
    private int HomedestPoint = 0;

    

    //Time for SuperPower
    float timepassed2;
    public int tenSec2;

  
    void Awake()
    {
        GetComponent<AudioSource>().playOnAwake = false;

        agent = GetComponent<NavMeshAgent>();

        // Disabling auto-braking allows for continuous movement
        // between points (ie, the agent doesn't slow down as it
        // approaches a destination point).
        agent.autoBraking = false;
        agent.speed = 0.5f;

        checkEnemy = 1;
        timepassed2 = tenSec2 * 1;

        //Set the PlayerHealth
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        animator = GetComponent<Animator>();

    }
    void Start()
    {
        surface.BuildNavMesh();
    }


    void GotoNextPoint()
    {
        agent.speed = 0.5f;
        // Returns if no points have been set up
        if (points.Length == 0)
            return;

        // Set the agent to go to the currently selected destination.
        agent.destination = points[destPoint].position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint = (destPoint + 1) % points.Length;
    }

    void GotoHomePoint()
    {
        // Returns if no points have been set up
        if (HomePoints.Length == 0)
            return;

        // Set the agent to go to the currently selected destination.
        agent.destination = HomePoints[HomedestPoint].position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        HomedestPoint = (HomedestPoint + 1) % HomePoints.Length;
    }

    public void GotoBase()
    {
        // Returns if no points have been set up
        if (points.Length == 0)
            return;

        // Set the agent to go to the currently selected destination.
        agent.destination = points[destPoint].position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint = (destPoint + 1) % points.Length;
    }


    void Update()
    {
        //print(checkEnemy);
        StartCoroutine(DelayUntilTime());
        
        TimeSpan time = TimeSpan.FromSeconds(timepassed2);
       
        //SuperPower Mode Activated
        if ((superPowerEnable.TimerSwitch == true) && (timepassed2 > 0))
        {
   
            timepassed2 = timepassed2 - Time.deltaTime;
        }


        //SuperPower is over return to Patroling
        if (timepassed2 < 0.1) 
        {
       
            scriptManager.ReturnToNormal();
            
        }

        // Choose the next destination point when the agent gets
        // close to the current one.
       
        if ((timeISoff == true) && (checkEnemy == 1))
        {
            if (!agent.pathPending && agent.remainingDistance < 0.2f)
            {
                //Play Walk Animation
                animator.SetBool("isWalking", true);
                GotoNextPoint();
            }
                
        }
        else if ((timeISoff == true) && (checkEnemy == 2))
        {
            agent.speed = 2f;
            if (!agent.pathPending && agent.remainingDistance < 0.2f)
            {
                if(animOnce < 1)
                {
                    animator.SetBool("isDead", true);
                    StartCoroutine(AnimationFor2sec());
                }

                if(animOnce == 2)
                {
                    animator.SetBool("isDead", false);
                    GotoHomePoint();
                }
                
            }

        }
    }

    public IEnumerator DelayUntilTime()
    {
        //Waiting until the timer is over
        yield return new WaitForSeconds(7);
        timeISoff = true;
    }

    public IEnumerator AnimationFor2sec()
    {
        //Waiting until the timer is over
        animOnce = 2;
        yield return new WaitForSeconds(3);

    }


    void OnTriggerEnter(Collider other)
    {
        // SuperMode Behavior
        if ((other.CompareTag("CameraPlayer") && (scriptManager.check == 2)))
        {
            //Send enemy to home
            checkEnemy = 2;
            
            //Play Destroy enemy sound
            GetComponent<AudioSource>().clip = DestroyEnemy;
            GetComponent<AudioSource>().Play();
   
        }

        // Normal Mode Behavior
        if ((other.CompareTag("CameraPlayer") && (scriptManager.check == 1)))
        {
            //Play Hit sound by the enemy
            GetComponent<AudioSource>().clip = HitSound;
            GetComponent<AudioSource>().Play();

            print("Damage apo enermyPatroling , emeine:" + currentHealth);

            //Lose Health - Vibration
            Handheld.Vibrate();
            TakeDamage(10);

        }

        //Health = 0
        if (currentHealth <= 0)
        {
            print("gameover from EnemyPatroling");

            //GameOver Sound play
            GetComponent<AudioSource>().clip = GameOverSound;
            GetComponent<AudioSource>().Play();

            //GameOver Screen
            Handheld.Vibrate();
            GameOverCanvas.SetActive(true);
            ScoreCanvas.SetActive(false);

            //Stop the game
            Time.timeScale = 0f;
        }

    }

    //Health Bar is going down
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }


}