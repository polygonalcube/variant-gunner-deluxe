using UnityEngine;

[RequireComponent(typeof(EnemyMethodsComponent))]
[RequireComponent(typeof(HPComponent))]
[RequireComponent(typeof(HurtComponent))]
[RequireComponent(typeof(MoveComponent))]
[RequireComponent(typeof(ShootComponent))]

public class BeelineEnemy : MonoBehaviour
{
    /// <summary>
    /// The logic for the Beeliner enemy's behavior.
    ///
    /// The enemy should travel downwards, occasionally shooting at the player.
    /// </summary>
    
    private EnemyMethodsComponent enemyMethods;
    private MoveComponent mover;
    private ShootComponent shooter;
    
    private Transform playerTransform;

    void Awake()
    {
        enemyMethods = GetComponent<EnemyMethodsComponent>();
        mover = GetComponent<MoveComponent>();
        shooter = GetComponent<ShootComponent>();

        playerTransform = enemyMethods.FindPlayer()?.transform;
    }
    
    void Update()
    {
        mover.Move(Vector3.down);
        ShootAtPlayer();
    }

    void ShootAtPlayer()
    {
        if (playerTransform == null)
        {
            playerTransform = enemyMethods.FindPlayer()?.transform;
        }
        Vector3 angleToPlayer = enemyMethods.AimAtPlayer(shooter.shotOrigin);

        shooter.shotAngle = angleToPlayer.x - 270f;
        if (playerTransform?.position.x > shooter.shotOrigin.position.x)
        {
            shooter.shotAngle = -shooter.shotAngle;
        }
        shooter.ShootAng(shooter.bulletSpeedAng);
    }
}
