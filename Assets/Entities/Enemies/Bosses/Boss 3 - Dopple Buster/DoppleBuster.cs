using UnityEngine;
using UnityEngine.SceneManagement;

public class DoppleBuster : MonoBehaviour
{
    public HPComponent healthManager;
    private HurtComponent hurtbox;
    public MoveComponent moveComponentEnter;
    public MoveComponent moveComponentAttack;
    public ShootComponent bomb;
    public ShootComponent bombUp;

    float timer;
    public Vector3 finalPosTop;
    public Vector3 finalPosBot;

    Transform player;
    
    enum States
    {
        Entering,
        ShootingDown,
        MovingDown,
        ShootingUp,
        MovingUp,
        Dying
    }
    States currentState = States.Entering;

    void Awake()
    {
        healthManager = GetComponent<HPComponent>();
        hurtbox = GetComponent<HurtComponent>();
    }

    void Start()
    {
        GameObject goPlayer = GameObject.Find("Player");
        if (goPlayer != null)
        {
            player = GameObject.Find("Player").transform;
        }
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
                    timer = 15f;
                    currentState = States.ShootingDown;
                }
                
                break;
            case States.ShootingDown:
                //movAttack.ySpeed = 0f;
                if (player != null)
                {
                    if (player.position.x < transform.position.x)
                    {
                        moveComponentAttack.Move(Vector3.left);
                    }
                    else if (player.position.x > transform.position.x)
                    {
                        moveComponentAttack.Move(Vector3.right);
                    }
                }

                bomb.Shoot(new Vector2(bomb.bulletSpeed.x, bomb.bulletSpeed.y));

                timer -= Time.deltaTime;
                if (timer <= 0f)
                {
                    currentState = States.MovingDown;
                }
                CheckHealth();
                
                break;
            case States.MovingDown:
                //movAttack.xSpeed = 0f;
                moveComponentAttack.Move(Vector3.down);
                if (transform.position.y < finalPosBot.y)
                {
                    transform.position = new Vector3(transform.position.x, finalPosBot.y, 0f);
                    timer = 15f;
                    currentState = States.ShootingUp;
                }
                CheckHealth();
                
                break;
            case States.ShootingUp:
                //movAttack.ySpeed = 0f;
                if (player != null)
                {
                    if (player.position.x < transform.position.x)
                    {
                        moveComponentAttack.Move(Vector3.left);
                    }
                    else if (player.position.x > transform.position.x)
                    {
                        moveComponentAttack.Move(Vector3.right);
                    }
                }

                bombUp.Shoot(new Vector2(bombUp.bulletSpeed.x, bombUp.bulletSpeed.y));

                timer -= Time.deltaTime;
                if (timer <= 0f)
                {
                    currentState = States.MovingUp;
                }
                CheckHealth();
                
                break;
            case States.MovingUp:
                moveComponentAttack.Move(Vector3.up);
                if (transform.position.y > finalPosTop.y)
                {
                    transform.position = new Vector3(transform.position.x, finalPosTop.y, 0f);
                    timer = 15f;
                    currentState = States.ShootingDown;
                }
                CheckHealth();
                
                break;
            case States.Dying:
                transform.eulerAngles += new Vector3(8f, 8f, 8f) * Time.deltaTime;
                
                timer -= Time.deltaTime;
                if (timer <= 0f)
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
            timer = 3f;
            hurtbox.isActive = false;
            currentState = States.Dying;
        }
    }
}
