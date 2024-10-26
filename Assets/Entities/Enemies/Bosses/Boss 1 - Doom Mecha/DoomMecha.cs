using System;
using UnityEngine;

[RequireComponent(typeof(EnemyMethodsComponent))]
[RequireComponent(typeof(HPComponent))]
[RequireComponent(typeof(MoveComponent))]
[RequireComponent(typeof(ShootComponent))]

public class DoomMecha : MonoBehaviour
{
    /// <summary>
    /// The logic for the Doom Mecha boss's attacks.
    /// </summary>

    private EnemyMethodsComponent enemyMethods;
    private HPComponent healthManager;
    private MoveComponent moveComponent;
    private ShootComponent vulcan;

    public float entranceSpeed;
    public Vector3 entranceAngle;
    public Vector3 finalPos;
    public Animator anim;
    public float timer;
    int cycle;
    public float timerSet;
    public float exitTimerSet;

    private Transform playerTransform;

    enum States
    {
        Entering,
        Stalling,
        Attacking,
        Dying
    }

    States currentState = States.Entering;

    void Awake()
    {
        enemyMethods = GetComponent<EnemyMethodsComponent>();
        healthManager = GetComponent<HPComponent>();
        moveComponent = GetComponent<MoveComponent>();
        vulcan = GetComponent<ShootComponent>();

        playerTransform = enemyMethods.FindPlayer()?.transform;
    }

    private void Start()
    {
        
    }

    void Update()
    {
        switch (currentState)
        {
            case States.Entering:
                transform.position = Vector3.MoveTowards(transform.position, finalPos, entranceSpeed * Time.deltaTime);
                transform.eulerAngles = entranceAngle;

                if (transform.position == finalPos)
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    timer = timerSet;
                    currentState = States.Stalling;
                }

                break;
            case States.Stalling:
                anim.Play("Idle");
                timer -= Time.deltaTime;

                if (timer <= 0)
                {
                    currentState = States.Attacking;
                }

                break;
            case States.Attacking:
                if (playerTransform == null)
                {
                    playerTransform = enemyMethods.FindPlayer().transform;
                }
                
                if (playerTransform?.position.x < transform.position.x - 0.5f)
                {
                    moveComponent.Move(Vector3.left);
                }
                else if (playerTransform?.position.x > transform.position.x + 0.5f)
                {
                    moveComponent.Move(Vector3.right);
                }
                else
                {
                    moveComponent.Move(Vector3.zero);
                }

                Vector3 currentPos = transform.position;
                Vector3 currentEuler = transform.eulerAngles;
                transform.position = new Vector3(vulcan.shotOrigin.position.x, vulcan.shotOrigin.position.y, 0f);
                transform.LookAt(playerTransform?.transform, Vector3.up);
                Vector3 pointer = transform.eulerAngles;
                transform.position = currentPos;
                transform.eulerAngles = currentEuler;

                if (vulcan.shotTimer <= 0)
                {
                    for (int i = 0; i < 5 - cycle; i++)
                    {
                        vulcan.shotAngle = pointer.x - 270f + ((float)(i - (2f - (0.5f * (float)cycle))) * 15f);
                        if (playerTransform?.position.x > vulcan.shotOrigin.position.x)
                        {
                            vulcan.shotAngle = -vulcan.shotAngle;
                        }

                        vulcan.ShootAng(vulcan.bulletSpeedAng);
                        vulcan.shotTimer = 0f;
                    }

                    vulcan.shotTimer = vulcan.shotTimerSet;
                    cycle = (int)Mathf.Repeat(cycle + 1, 2);
                }

                anim.Play("Shoot");

                if (healthManager.IsDead())
                {
                    timer = exitTimerSet;
                    currentState = States.Dying;
                }

                break;
            case States.Dying:
                anim.Play("Entrance");
                timer -= Time.deltaTime;
                transform.eulerAngles += new Vector3(5f, 5f, 5f) * Time.deltaTime;

                if (timer <= 0)
                {
                    GameManager.gm.level++;
                    Debug.Log("Going to level 2!");
                    Destroy(gameObject);
                }

                break;
        }
    }
}
