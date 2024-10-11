using UnityEngine;

public class DoomMecha : MonoBehaviour
{
    public ShootComponent vulcan;
    public MoveComponent mov;
    public HPComponent healthManager;

    bool isAttacking;
    bool isEntering;
    public bool isExiting;
    public float entranceSpeed;
    public Vector3 entranceAngle;
    public Vector3 finalPos;
    public Animator anim;
    public float timer;
    float cycle;
    public float timerSet;
    public float exitTimerSet;

    public Transform player;

    void Start()
    {
        isAttacking = false;
        isEntering = true;
        GameObject goPlayer = GameObject.Find("Player");
        if (goPlayer != null)
        {
            player = GameObject.Find("Player").transform;
        }
    }
    
    void Update()
    {
        if (isEntering)
        {
            transform.position = Vector3.MoveTowards(transform.position, finalPos, entranceSpeed * Time.deltaTime);
            transform.eulerAngles = entranceAngle;
            if (transform.position == finalPos)
            {
                isEntering = false;
                timer = timerSet;
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }

        if (!isAttacking && !isEntering)
        {
            anim.Play("Idle");
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                isAttacking = true;
            }
        }

        if (isAttacking && healthManager.currenthealth <= 0)
        {
            timer = exitTimerSet;
            isExiting = true;
            isAttacking = false;
        }
        else if (isAttacking && player != null)
        {
            if (player.position.x < transform.position.x - .5f)
            {
                mov.xSpeed = mov.Accelerate(mov.xSpeed, true);
            }
            else if (player.position.x > transform.position.x + .5f)
            {
                mov.xSpeed = mov.Accelerate(mov.xSpeed, false);
            }
            mov.xSpeed = mov.Cap(mov.xSpeed);
            mov.Move();

            Vector3 currentPos = transform.position;
            Vector3 currentEuler = transform.eulerAngles;
            transform.position = new Vector3(vulcan.shotOrigin.position.x, vulcan.shotOrigin.position.y, 0f);
            transform.LookAt(player.transform, Vector3.up);
            Vector3 pointer = transform.eulerAngles;
            transform.position = currentPos;
            transform.eulerAngles = currentEuler;
            
            /*
            Vector3 pointVec = player.position - vulcan.shotOrigin.position;
            pointVec = pointVec * (vulcan.bulletSpeed.y/pointVec.magnitude);
            */

            if (vulcan.shotTimer <= 0)
            {
                if (cycle == 1)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        vulcan.shotAngle = pointer.x - 270f + (((float)i-1.5f) * 15f);
                        if (player.position.x > vulcan.shotOrigin.position.x)
                        {
                            vulcan.shotAngle = -vulcan.shotAngle;
                        }
                        vulcan.ShootAng(vulcan.bulletSpeedAng);
                        vulcan.shotTimer = 0f;
                    }
                    vulcan.shotTimer = vulcan.shotTimerSet;
                    cycle = 0;
                }
                else
                {
                    for (int i = 0; i < 5; i++)
                    {
                        vulcan.shotAngle = pointer.x - 270f + ((float)(i-2) * 15f);
                        if (player.position.x > vulcan.shotOrigin.position.x)
                        {
                            vulcan.shotAngle = -vulcan.shotAngle;
                        }
                        vulcan.ShootAng(vulcan.bulletSpeedAng);
                        vulcan.shotTimer = 0f;
                    }
                    vulcan.shotTimer = vulcan.shotTimerSet;
                    cycle++;
                }
            }
            
            
            /*
            if ((player.position.x < transform.position.x - 1f) && transform.eulerAngles.z > -45f)
            {
                transform.eulerAngles -= new Vector3(0, 0, 5f * Time.deltaTime);
            }
            else if ((player.position.x > transform.position.x - 1f) && transform.eulerAngles.z < 45f)
            {
                transform.eulerAngles += new Vector3(0, 0, 5f * Time.deltaTime);
            }
            */
            anim.Play("Shoot");
            
        }

        if (isExiting)
        {
            anim.Play("Entrance");
            timer -= Time.deltaTime;
            transform.eulerAngles += new Vector3(5f, 5f, 5f) * Time.deltaTime;
            if (timer <= 0)
            {
                GameManager.gm.level++;
                Debug.Log("Going to level 2!");
                Destroy(this.gameObject);
            }
        }
    }
}
