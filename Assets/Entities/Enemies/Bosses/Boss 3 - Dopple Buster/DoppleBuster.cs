using UnityEngine;
using UnityEngine.SceneManagement;

public class DoppleBuster : MonoBehaviour
{
    public HPComponent hp;
    public MoveComponent moveComponentEnter;
    public MoveComponent moveComponentAttack;
    public ShootComponent bomb;
    public ShootComponent bombUp;

    int state; //0=enter, 1=bomb
    float timer;
    public Vector3 finalPosTop;
    public Vector3 finalPosBot;

    Transform player;

    void Start()
    {
        state = 0;
        GameObject goPlayer = GameObject.Find("Player");
        if (goPlayer != null)
        {
            player = GameObject.Find("Player").transform;
        }
    }

    void Update()
    {
        if (state == 0) //enter
        {
            moveComponentEnter.Move(Vector3.up);
            if (transform.position.y > finalPosTop.y)
            {
                transform.position = finalPosTop;
                timer = 15f;
                state++;
            }
        }
        if (state == 1) //bomb down
        {
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
                state++;
            }
            CheckHealth();
        }
        if (state == 2) //move to bottom
        {
            //movAttack.xSpeed = 0f;
            moveComponentAttack.Move(Vector3.down);
            if (transform.position.y < finalPosBot.y)
            {
                transform.position = new Vector3(transform.position.x, finalPosBot.y, 0f);
                timer = 15f;
                state++;
            }
            CheckHealth();
        }
        if (state == 3) //bomb up
        {
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
                state++;
            }
            CheckHealth();
        }
        if (state == 4) //move to top
        {
            moveComponentAttack.Move(Vector3.up);
            if (transform.position.y > finalPosTop.y)
            {
                transform.position = new Vector3(transform.position.x, finalPosTop.y, 0f);
                timer = 15f;
                state = 1;
            }
            CheckHealth();
        }
        if (state == 5)
        {
            timer -= Time.deltaTime;
            transform.eulerAngles += new Vector3(8f, 8f, 8f) * Time.deltaTime;
            if (timer <= 0f)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                Destroy(this.gameObject);
            }
        }
    }

    void CheckHealth()
    {
        if (hp.currenthealth <= 0)
        {
            timer = 3f;
            state = 5;
        }
    }
}
