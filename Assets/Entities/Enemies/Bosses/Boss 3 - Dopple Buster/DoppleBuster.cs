using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(EnemyMethodsComponent))]
[RequireComponent(typeof(HPComponent))]
[RequireComponent(typeof(HurtComponent))]

public class DoppleBuster : MonoBehaviour
{
    private EnemyMethodsComponent enemyMethods;
    private HPComponent healthManager;
    private HurtComponent hurtbox;
    [SerializeField] private MoveComponent moveComponentEnter;
    [SerializeField] private MoveComponent moveComponentAttack;
    [SerializeField] private ShootComponent bomb;
    [SerializeField] private ShootComponent bombUp;

    private float stateChangeTimer;
    [SerializeField] private float stateChangeTimerSet = 10f;
    [SerializeField] private float exitTimerSet = 3f;
    
    [SerializeField] private Vector3 finalPosTop = new(0f, 4f, 0f);
    [SerializeField] private Vector3 finalPosBot = new(0f, -4f, 0f);

    private Transform playerTransform;

    private bool movingToTopNext;
    
    enum States
    {
        Entering,
        Shooting,
        Positioning,
        Dying
    }
    States currentState = States.Entering;

    void Awake()
    {
        enemyMethods = GetComponent<EnemyMethodsComponent>();
        healthManager = GetComponent<HPComponent>();
        hurtbox = GetComponent<HurtComponent>();
        
        playerTransform = enemyMethods.FindPlayer()?.transform;
    }

    void Update()
    {
        switch (currentState)
        {
            case States.Entering:
                moveComponentEnter.Move(Vector3.up);
            
                if (transform.position.y > finalPosTop.y)
                {
                    transform.position = finalPosTop;
                    stateChangeTimer = stateChangeTimerSet;
                    currentState = States.Shooting;
                }
                
                break;
            case States.Shooting:
                moveComponentAttack.currentSpeedY = 0f;
                
                if (playerTransform != null)
                {
                    if (playerTransform?.position.x < transform.position.x)
                    {
                        moveComponentAttack.Move(Vector3.left);
                    }
                    else if (playerTransform?.position.x > transform.position.x)
                    {
                        moveComponentAttack.Move(Vector3.right);
                    }
                    else
                    {
                        moveComponentAttack.Move(Vector3.zero);
                    }
                }

                if (movingToTopNext)
                {
                    bombUp.Shoot(new Vector2(bombUp.bulletSpeed.x, bombUp.bulletSpeed.y));
                }
                else
                {
                    bomb.Shoot(new Vector2(bomb.bulletSpeed.x, bomb.bulletSpeed.y));
                }

                CheckHealth();
                
                stateChangeTimer -= Time.deltaTime;
                if (stateChangeTimer <= 0f)
                {
                    currentState = States.Positioning;
                }
                
                break;
            case States.Positioning:
                if (movingToTopNext)
                {
                    moveComponentAttack.Move(Vector3.up);
                    
                    if (transform.position.y > finalPosTop.y)
                    {
                        transform.position = new Vector3(transform.position.x, finalPosTop.y, 0f);
                        stateChangeTimer = stateChangeTimerSet;
                        movingToTopNext = !movingToTopNext;
                        currentState = States.Shooting;
                    }
                }
                else
                {
                    moveComponentAttack.Move(Vector3.down);
                    
                    if (transform.position.y < finalPosBot.y)
                    {
                        transform.position = new Vector3(transform.position.x, finalPosBot.y, 0f);
                        stateChangeTimer = stateChangeTimerSet;
                        movingToTopNext = !movingToTopNext;
                        currentState = States.Shooting;
                    }
                }
                
                CheckHealth();
                
                break;
            /*case States.ShootingUp:
                moveComponentAttack.currentSpeedY = 0f;
                if (playerTransform != null)
                {
                    if (playerTransform.position.x < transform.position.x)
                    {
                        moveComponentAttack.Move(Vector3.left);
                    }
                    else if (playerTransform.position.x > transform.position.x)
                    {
                        moveComponentAttack.Move(Vector3.right);
                    }
                    else
                    {
                        moveComponentAttack.Move(Vector3.zero);
                    }
                }

                bombUp.Shoot(new Vector2(bombUp.bulletSpeed.x, bombUp.bulletSpeed.y));

                CheckHealth();
                
                stateChangeTimer -= Time.deltaTime;
                if (stateChangeTimer <= 0f)
                {
                    currentState = States.MovingUp;
                }
                
                break;
            case States.MovingUp:
                moveComponentAttack.Move(Vector3.up);
                
                CheckHealth();
                if (transform.position.y > finalPosTop.y)
                {
                    transform.position = new Vector3(transform.position.x, finalPosTop.y, 0f);
                    stateChangeTimer = stateChangeTimerSet;
                    currentState = States.Shooting;
                }
                
                break;*/
            case States.Dying:
                transform.eulerAngles += new Vector3(8f, 8f, 8f) * Time.deltaTime;
                
                stateChangeTimer -= Time.deltaTime;
                if (stateChangeTimer <= 0f)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                    Destroy(gameObject);
                }
                
                break;
        }
    }

    void CheckHealth()
    {
        if (healthManager.IsDead())
        {
            stateChangeTimer = exitTimerSet;
            hurtbox.isActive = false;
            currentState = States.Dying;
        }
    }
}
