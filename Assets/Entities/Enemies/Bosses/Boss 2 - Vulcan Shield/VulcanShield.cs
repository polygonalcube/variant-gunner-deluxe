using UnityEngine;

[RequireComponent(typeof(EnemyMethodsComponent))]
[RequireComponent(typeof(HPComponent))]
[RequireComponent(typeof(HurtComponent))]
[RequireComponent(typeof(MoveComponent))]
[RequireComponent(typeof(ShootComponent))]

public class VulcanShield : MonoBehaviour
{
    private EnemyMethodsComponent enemyMethods;
    private HPComponent healthManager;
    private HurtComponent hurtbox;
    private MoveComponent moveComponent;
    private ShootComponent shooter;

    [SerializeField] private float entranceSpeed = 6f;
    [SerializeField] private Vector3 finalPosition = Vector3.zero;
    [SerializeField] private Animator anim;
    private float stateChangeTimer;
    [SerializeField] private float stateChangeTimerSet = 3f;
    [SerializeField] private int bulletCount = 60;

    private Transform playerTransform;

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
        enemyMethods = GetComponent<EnemyMethodsComponent>();
        healthManager = GetComponent<HPComponent>();
        hurtbox = GetComponent<HurtComponent>();
        moveComponent = GetComponent<MoveComponent>();
        shooter = GetComponent<ShootComponent>();

        playerTransform = enemyMethods.FindPlayer()?.transform;
    }
    
    void Start()
    {
        hurtbox.isActive = false;
    }
    
    void Update()
    {
        switch (currentState)
        {
            case States.Entering:
                transform.position = Vector3.MoveTowards(transform.position, finalPosition, entranceSpeed * Time.deltaTime);
                if (transform.position == finalPosition)
                {
                    stateChangeTimer = stateChangeTimerSet;
                    hurtbox.isActive = false;
                    currentState = States.Positioning;
                }
                
                break;
            case States.Positioning:
                anim.Play("defend");
                
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
                
                stateChangeTimer -= Time.deltaTime;
                if (healthManager.IsDead())
                {
                    stateChangeTimer = stateChangeTimerSet;
                    hurtbox.isActive = false;
                    currentState = States.Dying;
                }
                else if (stateChangeTimer <= 0f)
                {
                    stateChangeTimer = stateChangeTimerSet;
                    hurtbox.isActive = true;
                    currentState = States.Attacking;
                }
                
                break;
            case States.Attacking:
                anim.Play("attack");
                
                foreach (Collider shield in shields)
                {
                    shield.enabled = false;
                }
                
                if (shooter.shotTimer <= 0)
                {
                    for (int i = 0; i < bulletCount; i++)
                    {
                        shooter.shotAngle = 360f/(float)bulletCount*(float)i;
                        shooter.ShootAng(shooter.bulletSpeedAng);
                        shooter.shotTimer = 0f;
                    }
                    shooter.shotTimer = shooter.shotTimerSet;
                }
                
                stateChangeTimer -= Time.deltaTime;
                if (healthManager.IsDead())
                {
                    stateChangeTimer = stateChangeTimerSet;
                    hurtbox.isActive = false;
                    currentState = States.Dying;
                }
                else if (stateChangeTimer <= 0f)
                {
                    stateChangeTimer = stateChangeTimerSet;
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
                
                stateChangeTimer -= Time.deltaTime;
                if (stateChangeTimer <= 0)
                {
                    Destroy(gameObject);
                }
                
                break;
        }
    }
}
