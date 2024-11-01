using UnityEngine;

[RequireComponent(typeof(EnemyMethodsComponent))]
[RequireComponent(typeof(HPComponent))]
[RequireComponent(typeof(HurtComponent))]
[RequireComponent(typeof(ShootComponent))]

public class BarrelerEnemy : MonoBehaviour
{
    private EnemyMethodsComponent enemyMethods;
    [SerializeField] private MoveComponent moveComponentEnter;
    [SerializeField] private MoveComponent moveComponentAttack;
    private ShootComponent shooter;

    private Transform playerTransform;

    public Vector3 attackMoveDirection;

    enum States
    {
        Entering,
        Barrelling
    }

    States currentState = States.Entering;

    void Awake()
    {
        enemyMethods = GetComponent<EnemyMethodsComponent>();
        shooter = GetComponent<ShootComponent>();

        playerTransform = enemyMethods.FindPlayer()?.transform;
    }

    void Update()
    {
        switch (currentState)
        {
            case States.Entering:
                moveComponentEnter.Move(Vector3.up);
                
                if (playerTransform == null)
                {
                    playerTransform = enemyMethods.FindPlayer()?.transform;
                }
                
                if (transform.position.y > -5f)
                {
                    Vector3 playerPos = playerTransform.position;

                    Vector3 pointVec = playerPos - transform.position;
                    pointVec *= (moveComponentAttack.maximumSpeed.x / pointVec.magnitude);
                    attackMoveDirection = new Vector3(pointVec.x, pointVec.y, 0f);

                    moveComponentAttack.currentSpeedX = pointVec.x;
                    moveComponentAttack.currentSpeedY = pointVec.y;

                    currentState = States.Barrelling;
                }
                
                break;
            case States.Barrelling:
                moveComponentAttack.Move(attackMoveDirection);
                shooter.ShootAng(0f);
                
                break;
        }
    }
}
