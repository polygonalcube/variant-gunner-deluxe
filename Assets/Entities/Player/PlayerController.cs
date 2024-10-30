using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public HPComponent hp;
    public HurtComponent hbox;
    public MoveComponent mov;

    public ShootComponent vulcan;
    public ShootComponent laser;
    public ShootComponent laser1;
    public ShootComponent homing0;
    public ShootComponent homing1;
    public ShootComponent bomb;

    public Vector3 xy = Vector3.zero;

    public bool vulcanPerformed;
    public bool laserPerformed;
    public bool homingPerformed;
    public bool bombPerformed;

    public InputAction movement;
    public InputAction shootVulcan;
    public InputAction shootLaser;
    public InputAction shootHoming;
    public InputAction shootBomb;

    void OnEnable()
    {
        movement.Enable();

        shootVulcan.Enable();
        shootVulcan.performed += ctx => vulcanPerformed = true;
        shootLaser.Enable();
        shootLaser.performed += ctx => laserPerformed = true;
        shootHoming.Enable();
        shootHoming.performed += ctx => homingPerformed = true;
        shootBomb.Enable();
        shootBomb.performed += ctx => bombPerformed = true;
    }

    void OnDisable()
    {
        movement.Disable();
        shootVulcan.Disable();
        shootLaser.Disable();
        shootHoming.Disable();
        shootBomb.Disable();
    }

    void Update()
    {
        ReceiveInput();
        Movement();
        Shooting();
        ResetInput();
        WhenDying();
    }

    void Movement()
    {
        mov.Move(xy);

        //mov.xSpeed = mov.CheckNearZero(mov.xSpeed);
        //mov.ySpeed = mov.CheckNearZero(mov.ySpeed);

        mov.BoundXY(7.2f, 5.3f);
    }

    void Shooting()
    {
        InputAction[] shootAction = {shootVulcan, shootLaser, shootHoming, shootBomb};
        ShootComponent[] shooter = {vulcan, laser, homing0, bomb, homing1, laser1};
        for (int i = 0; i < 4; i++)
        {
            if (shootAction[i].IsPressed())
            {
                if (i == 2) //homing
                {
                    shooter[i].Shoot(new Vector2(shooter[i].bulletSpeed.x, shooter[i].bulletSpeed.y));
                    shooter[4].Shoot(new Vector2(shooter[4].bulletSpeed.x, shooter[4].bulletSpeed.y));
                }
                else if (i == 1) //laser
                {
                    shooter[i].ShootAng(shooter[i].bulletSpeedAng);
                    shooter[5].ShootAng(shooter[5].bulletSpeedAng);
                }
                else
                {
                    shooter[i].Shoot(new Vector2(shooter[i].bulletSpeed.x, shooter[i].bulletSpeed.y));
                }

                //sfx[1].Play(); //Play shoot sound
                break;
            }
        }
    }

    void ReceiveInput()
    {
        xy = movement.ReadValue<Vector2>();
    }

    void ResetInput()
    {
        //resets the input booleans
        vulcanPerformed = false;
        laserPerformed = false;
        homingPerformed = false;
        bombPerformed = false;
    }
    
    void WhenDying()
    {
        if (hp.IsDead())
        {
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        yield return new WaitForEndOfFrame();
        Destroy(gameObject);
    }
}
