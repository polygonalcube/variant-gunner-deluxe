using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class VulcanShield : MonoBehaviour
{
    public ShootComponent bulletOrigin;
    public HPComponent healthManager;
    public HurtComponent hurtbox;
    public MoveComponent moveComponent;

    public float entranceSpeed;
    public Vector3 finalPos; //0,0,0
    public Animator anim;
    public float timer;
    public float timerSet;
    public float bulletCount;

    public Transform playerTransform;

    public Collider[] shields;
    
    enum States
    {
        Entering,
        Positioning,
        Attacking,
        Dying
    }
    States currentState = States.Entering;
    
    void Awake()
    {
        //enemyMethods = GetComponent<EnemyMethodsComponent>();
        healthManager = GetComponent<HPComponent>();
        hurtbox = GetComponent<HurtComponent>();
        moveComponent = GetComponent<MoveComponent>();
        bulletOrigin = GetComponent<ShootComponent>();

        //playerTransform = enemyMethods.FindPlayer()?.transform;
    }
    
    void Start()
    {
        GameObject goPlayer = GameObject.Find("Player");
        if (goPlayer != null)
        {
            playerTransform = GameObject.Find("Player").transform;
        }
        
        hurtbox.isActive = false;
    }
    
    void Update()
    {
        switch (currentState)
        {
            case States.Entering:
                transform.position = Vector3.MoveTowards(transform.position, finalPos, entranceSpeed * Time.deltaTime);
                if (transform.position == finalPos)
                {
                    timer = timerSet;
                    hurtbox.isActive = false;
                    currentState = States.Positioning;
                }
                
                break;
            case States.Positioning:
                anim.Play("defend");
                
                timer -= Time.deltaTime;
                
                foreach (Collider shield in shields)
                {
                    shield.enabled = true;
                }
                
                if (playerTransform?.position.x < transform.position.x - 0.1f)
                {
                    moveComponent.Move(Vector3.left);
                }
                else if (playerTransform?.position.x > transform.position.x + 0.1f)
                {
                    moveComponent.Move(Vector3.right);
                }
                else
                {
                    moveComponent.Move(Vector3.zero);
                }
                
                if (healthManager.IsDead())
                {
                    timer = timerSet;
                    hurtbox.isActive = false;
                    currentState = States.Dying;
                }
                else if (timer <= 0f)
                {
                    timer = timerSet;
                    hurtbox.isActive = true;
                    currentState = States.Attacking;
                }
                
                break;
            case States.Attacking:
                anim.Play("attack");

                timer -= Time.deltaTime;
                
                foreach (Collider shield in shields)
                {
                    shield.enabled = false;
                }
                
                if (bulletOrigin.shotTimer <= 0)
                {
                    for (int i = 0; i < bulletCount; i++)
                    {
                        bulletOrigin.shotAngle = 360f/bulletCount*(float)i;
                        bulletOrigin.ShootAng(bulletOrigin.bulletSpeedAng);
                        bulletOrigin.shotTimer = 0f;
                    }
                    bulletOrigin.shotTimer = bulletOrigin.shotTimerSet;
                    timer = timerSet;
                }
                
                if (healthManager.IsDead())
                {
                    timer = timerSet;
                    hurtbox.isActive = false;
                    currentState = States.Dying;
                }
                else if (timer <= 0f)
                {
                    timer = timerSet;
                    hurtbox.isActive = false;
                    currentState = States.Positioning;
                }
                /*if (hp.currenthealth <= 0)
                {
                    state = 1;
                    timer = timerSet;
                }*/
                
                break;
            case States.Dying:
                transform.eulerAngles += new Vector3(8f, 8f, 8f) * Time.deltaTime;
                
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    Destroy(gameObject);
                }
                
                break;
        }
    }
}
