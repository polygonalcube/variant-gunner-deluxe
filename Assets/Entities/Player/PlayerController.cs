using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(HPComponent))]
[RequireComponent(typeof(MoveComponent))]

public class PlayerController : MonoBehaviour
{
    [SerializeField] private HPComponent healthManager;
    [SerializeField] private MoveComponent mover;

    public ShootComponent vulcan;
    public ShootComponent laser;
    public ShootComponent laser1;
    public ShootComponent homing0;
    public ShootComponent homing1;
    public ShootComponent bomb;

    private Vector2 movementDirection = Vector2.zero;

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

    private void Awake()
    {
        healthManager = GetComponent<HPComponent>();
        mover = GetComponent<MoveComponent>();
    }

    void Update()
    {
        ReceiveInput();
        Movement();
        Shooting();
        ResetInputBooleans();
        WhenDying();
    }

    void Movement()
    {
        mover.Move(new Vector3(movementDirection.x, movementDirection.y, 0f));
        mover.BoundXY(7.2f, 5.3f);
    }

    void Shooting()
    {
        InputAction[] shootActions = { shootVulcan, shootLaser, shootHoming, shootBomb };
        ShootComponent[] shooters = { vulcan, laser, homing0, bomb, homing1, laser1 };

        for (int i = 0; i < 4; i++)
        {
            if (shootActions[i].IsPressed())
            {
                bool usingHoming = i == 2;
                bool usingLaser = i == 1;

                if (usingHoming)
                {
                    shooters[i].Shoot(new Vector2(shooters[i].bulletSpeed.x, shooters[i].bulletSpeed.y));
                    shooters[4].Shoot(new Vector2(shooters[4].bulletSpeed.x, shooters[4].bulletSpeed.y));
                }
                else if (usingLaser)
                {
                    shooters[i].ShootAng(shooters[i].bulletSpeedAng);
                    shooters[5].ShootAng(shooters[5].bulletSpeedAng);
                }
                else
                {
                    shooters[i].Shoot(new Vector2(shooters[i].bulletSpeed.x, shooters[i].bulletSpeed.y));
                }

                break;
            }
        }
    }

    void ReceiveInput()
    {
        movementDirection = movement.ReadValue<Vector2>();
    }

    void ResetInputBooleans()
    {
        vulcanPerformed = false;
        laserPerformed = false;
        homingPerformed = false;
        bombPerformed = false;
    }

    void WhenDying()
    {
        if (healthManager.IsDead())
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
