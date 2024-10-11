using System.Collections;
using System.Collections.Generic;
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

    void Start()
    {
        //eh?
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
        if(xy.x < -0.1)
        {
            mov.xSpeed = mov.Accelerate(mov.xSpeed, true);
        }
        else if(xy.x > 0.1)
        {
            mov.xSpeed = mov.Accelerate(mov.xSpeed, false);
        }

        if(xy.y < -0.1)
        {
            mov.ySpeed = mov.Accelerate(mov.ySpeed, true);
        }
        else if(xy.y > 0.1)
        {
            mov.ySpeed = mov.Accelerate(mov.ySpeed, false);
        }
        
        if (xy.x == 0) // if neither is true, deaccelerate the player
        {
            if(mov.xSpeed > 0)
            {
                mov.xSpeed = mov.Decelerate(mov.xSpeed, true);
            }
            if(mov.xSpeed < 0)
            {
                mov.xSpeed = mov.Decelerate(mov.xSpeed, false);
            }
            
        }
        if (xy.y == 0)
        {
            if(mov.ySpeed > 0)
            {
                mov.ySpeed = mov.Decelerate(mov.ySpeed, true);
            }
            if(mov.ySpeed < 0)
            {
                mov.ySpeed = mov.Decelerate(mov.ySpeed, false);
            }
        }
        mov.xSpeed = mov.CheckNearZero(mov.xSpeed);
        mov.ySpeed = mov.CheckNearZero(mov.ySpeed);
        
        //caps movement speed
        mov.xSpeed = mov.Cap(mov.xSpeed);
        mov.ySpeed = mov.Cap(mov.ySpeed);

        mov.Move();

        mov.BoundXY(7.2f, 5.3f);
    }

    void Shooting()
    {
        InputAction[] shootAction = new InputAction[] {shootVulcan, shootLaser, shootHoming, shootBomb};
        ShootComponent[] shooter = new ShootComponent[] {vulcan, laser, homing0, bomb, homing1, laser1};
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
        if (hp.currenthealth <= 0) StartCoroutine(Die());
    }

    IEnumerator Die()
    {
        yield return new WaitForEndOfFrame();
        Destroy(gameObject);
    }
}
