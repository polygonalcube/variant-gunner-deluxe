using UnityEngine;

[RequireComponent(typeof(EnemyMethodsComponent))]
[RequireComponent(typeof(HPComponent))]
[RequireComponent(typeof(HurtComponent))]
[RequireComponent(typeof(MoveComponent))]
[RequireComponent(typeof(ShootComponent))]

public class DoomMecha : MonoBehaviour
{
    /// <summary>
    /// The logic for the Doom Mecha boss's attacks.
    /// </summary>

    private EnemyMethodsComponent enemyMethods;
    private HPComponent healthManager;
    private HurtComponent hurtbox;
    private MoveComponent moveComponent;
    private ShootComponent shooter;

    [SerializeField] private float entranceSpeed = 200f;
    [SerializeField] private Vector3 entranceAngle = new(-50f, -30f, 0f);
    [SerializeField] private Vector3 finalPosition = Vector3.zero;
    [SerializeField] private Animator animator;
    private float stateChangeTimer;
    private int cycle;
    [SerializeField] private float stateChangeTimerSet = 1f;
    [SerializeField] private float exitTimerSet = 3f;

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
        hurtbox = GetComponent<HurtComponent>();
        moveComponent = GetComponent<MoveComponent>();
        shooter = GetComponent<ShootComponent>();

        playerTransform = enemyMethods.FindPlayer()?.transform;
    }

    void Update()
    {
        switch (currentState)
        {
            case States.Entering:
                transform.position = Vector3.MoveTowards(transform.position, finalPosition, entranceSpeed * Time.deltaTime);
                transform.eulerAngles = entranceAngle;

                if (transform.position == finalPosition)
                {
                    transform.eulerAngles = Vector3.zero;
                    stateChangeTimer = stateChangeTimerSet;
                    currentState = States.Stalling;
                }

                break;
            case States.Stalling:
                animator.Play("Idle");
                stateChangeTimer -= Time.deltaTime;

                if (stateChangeTimer <= 0)
                {
                    currentState = States.Attacking;
                }

                break;
            case States.Attacking:
                if (playerTransform == null)
                {
                    playerTransform = enemyMethods.FindPlayer()?.transform;
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

                Vector3 angleToPlayer = enemyMethods.AimAtPlayer(shooter.shotOrigin);
                if (shooter.shotTimer <= 0)
                {
                    for (int i = 0; i < 5 - cycle; i++)
                    {
                        shooter.shotAngle = angleToPlayer.x - 270f + ((float)(i - (2f - (0.5f * (float)cycle))) * 15f);
                        if (playerTransform?.position.x > shooter.shotOrigin.position.x)
                        {
                            shooter.shotAngle = -shooter.shotAngle;
                        }

                        shooter.ShootAng(shooter.bulletSpeedAng);
                        shooter.shotTimer = 0f;
                    }

                    shooter.shotTimer = shooter.shotTimerSet;
                    cycle = (int)Mathf.Repeat(cycle + 1, 2);
                }

                animator.Play("Shoot");

                if (healthManager.IsDead())
                {
                    stateChangeTimer = exitTimerSet;
                    hurtbox.isActive = false;
                    currentState = States.Dying;
                }

                break;
            case States.Dying:
                animator.Play("Entrance");
                
                transform.eulerAngles += new Vector3(5f, 5f, 5f) * Time.deltaTime;
                
                stateChangeTimer -= Time.deltaTime;
                if (stateChangeTimer <= 0)
                {
                    GameManager.gm.level = 2;
                    Debug.Log("Going to level 2!");
                    Destroy(gameObject);
                }

                break;
        }
    }
}
